using LibrarySeatReservation.Web.Models.ViewModels;
using LibrarySeatReservation.Web.Models.ViewModels.Admin;

namespace LibrarySeatReservation.Web.Services;

public class ReservationResult
{
    public bool Success { get; set; }
    public int ReservationId { get; set; }
    public string? ErrorMessage { get; set; }

    public static ReservationResult Ok(int id) => new() { Success = true, ReservationId = id };
    public static ReservationResult Fail(string message) => new() { Success = false, ErrorMessage = message };
}

public interface IReservationService
{
    // 用户端
    ReservationResult CreateReservation(int seatId, string userId, DateTime date, int timeSlot);
    List<BookingItem> GetUserBookings(string userId);
    bool CancelReservation(int reservationId, string userId);

    // 管理端
    List<AdminBookingItem> GetAllBookings();
}
