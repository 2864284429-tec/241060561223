using LibrarySeatReservation.Web.DataAccess;
using LibrarySeatReservation.Web.Models.Entities;
using LibrarySeatReservation.Web.Models.ViewModels;
using LibrarySeatReservation.Web.Models.ViewModels.Admin;
using Microsoft.EntityFrameworkCore;

namespace LibrarySeatReservation.Web.Services;

public class ReservationService : IReservationService
{
    private readonly AppDbContext _db;

    public ReservationService(AppDbContext db)
    {
        _db = db;
    }

    public ReservationResult CreateReservation(int seatId, string userId, DateTime date, int timeSlot)
    {
        var seat = _db.Seats.Find(seatId);
        if (seat == null)
            return ReservationResult.Fail("座位不存在");

        if (!seat.IsAvailable)
            return ReservationResult.Fail("座位已停用");

        // 检查时段范围
        if (timeSlot < 8 || timeSlot > 20)
            return ReservationResult.Fail("时段无效");

        // 检查日期范围（今天~后天）
        var today = DateTime.Today;
        var maxDate = today.AddDays(2);
        if (date.Date < today || date.Date > maxDate)
            return ReservationResult.Fail("只能预约今天至后天的时段");

        // 检查时段是否已过时（仅限今天）
        if (date.Date == today && DateTime.Now.Hour >= timeSlot)
            return ReservationResult.Fail("该时段已过时");

        // 冲突检测：同一座位同一日期同一时段不能有重复"已预约"
        var conflict = _db.Reservations.Any(r =>
            r.SeatId == seatId &&
            r.Date == date.Date &&
            r.TimeSlot == timeSlot &&
            r.Status == "已预约");

        if (conflict)
            return ReservationResult.Fail("该时段已被预约");

        var reservation = new Reservation
        {
            SeatId = seatId,
            UserId = userId,
            Date = date.Date,
            TimeSlot = timeSlot,
            Status = "已预约",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _db.Reservations.Add(reservation);
        _db.SaveChanges();

        return ReservationResult.Ok(reservation.Id);
    }

    public List<BookingItem> GetUserBookings(string userId)
    {
        var now = DateTime.Now;
        var today = DateTime.Today;

        return _db.Reservations
            .Include(r => r.Seat)
            .Where(r => r.UserId == userId)
            .OrderByDescending(r => r.Date)
            .ThenByDescending(r => r.TimeSlot)
            .Select(r => new BookingItem
            {
                ReservationId = r.Id,
                SeatName = r.Seat!.Name,
                SeatLocation = r.Seat.Location,
                Date = r.Date,
                TimeSlot = r.TimeSlot,
                Status = r.Status,
                // "已过时"：动态计算，当前时间 > 时段结束时间
                CanCancel = r.Status == "已预约" &&
                    (r.Date > today || (r.Date == today && r.TimeSlot > now.Hour))
            })
            .ToList();
    }

    public bool CancelReservation(int reservationId, string userId)
    {
        var reservation = _db.Reservations
            .FirstOrDefault(r => r.Id == reservationId && r.UserId == userId);

        if (reservation == null) return false;
        if (reservation.Status != "已预约") return false;

        // 检查是否已过时
        var slotEndTime = reservation.Date.AddHours(reservation.TimeSlot + 1);
        if (DateTime.Now >= slotEndTime) return false;

        reservation.Status = "已取消";
        reservation.UpdatedAt = DateTime.UtcNow;
        return _db.SaveChanges() > 0;
    }

    public List<AdminBookingItem> GetAllBookings()
    {
        return _db.Reservations
            .Include(r => r.Seat)
            .Include(r => r.DemoUser)
            .OrderByDescending(r => r.Date)
            .ThenByDescending(r => r.TimeSlot)
            .Select(r => new AdminBookingItem
            {
                ReservationId = r.Id,
                UserName = r.DemoUser!.Name,
                SeatName = r.Seat!.Name,
                Date = r.Date,
                TimeSlot = r.TimeSlot,
                Status = r.Status
            })
            .ToList();
    }
}
