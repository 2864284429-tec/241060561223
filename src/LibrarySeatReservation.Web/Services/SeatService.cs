using LibrarySeatReservation.Web.DataAccess;
using LibrarySeatReservation.Web.Models.Entities;
using LibrarySeatReservation.Web.Models.ViewModels;
using LibrarySeatReservation.Web.Models.ViewModels.Admin;
using Microsoft.EntityFrameworkCore;

namespace LibrarySeatReservation.Web.Services;

public class SeatService : ISeatService
{
    private readonly AppDbContext _db;

    public SeatService(AppDbContext db)
    {
        _db = db;
    }

    public List<SeatListItem> GetAllSeatsWithStatus()
    {
        var today = DateTime.Today;
        var currentHour = DateTime.Now.Hour;

        return _db.Seats
            .Select(seat => new SeatListItem
            {
                Id = seat.Id,
                Name = seat.Name,
                Location = seat.Location,
                Features = seat.Features,
                IsAvailable = seat.IsAvailable,
                CurrentStatus = !seat.IsAvailable ? "不可用" :
                    _db.Reservations.Any(r =>
                        r.SeatId == seat.Id &&
                        r.Date == today &&
                        r.TimeSlot == currentHour &&
                        r.Status == "已预约") ? "已预约" : "可用"
            })
            .ToList();
    }

    public SeatDetailViewModel? GetSeatDetail(int seatId)
    {
        var seat = _db.Seats.Find(seatId);
        if (seat == null) return null;

        return new SeatDetailViewModel
        {
            Id = seat.Id,
            Name = seat.Name,
            Location = seat.Location,
            Capacity = seat.Capacity,
            Features = seat.Features,
            IsAvailable = seat.IsAvailable,
            CanReserve = seat.IsAvailable
        };
    }

    public List<AdminSeatItem> GetAllSeats()
    {
        return _db.Seats.Select(seat => new AdminSeatItem
        {
            Id = seat.Id,
            Name = seat.Name,
            Location = seat.Location,
            Capacity = seat.Capacity,
            Features = seat.Features,
            IsAvailable = seat.IsAvailable
        }).ToList();
    }

    public bool CreateSeat(AdminSeatItem model)
    {
        var seat = new Seat
        {
            Name = model.Name,
            Location = model.Location,
            Capacity = model.Capacity,
            Features = model.Features,
            IsAvailable = model.IsAvailable,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _db.Seats.Add(seat);
        return _db.SaveChanges() > 0;
    }

    public bool UpdateSeat(int id, AdminSeatItem model)
    {
        var seat = _db.Seats.Find(id);
        if (seat == null) return false;

        seat.Name = model.Name;
        seat.Location = model.Location;
        seat.Capacity = model.Capacity;
        seat.Features = model.Features;
        seat.IsAvailable = model.IsAvailable;
        seat.UpdatedAt = DateTime.UtcNow;

        return _db.SaveChanges() > 0;
    }

    public bool DeleteSeat(int seatId)
    {
        var seat = _db.Seats.Find(seatId);
        if (seat == null) return false;

        // 检查是否有未完成的预约
        var hasActiveReservations = _db.Reservations.Any(r =>
            r.SeatId == seatId && r.Status == "已预约");
        if (hasActiveReservations) return false;

        _db.Seats.Remove(seat);
        return _db.SaveChanges() > 0;
    }
}
