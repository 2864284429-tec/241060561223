using LibrarySeatReservation.Web.Models.ViewModels;
using LibrarySeatReservation.Web.Models.ViewModels.Admin;

namespace LibrarySeatReservation.Web.Services;

public interface ISeatService
{
    // 用户端
    List<SeatListItem> GetAllSeatsWithStatus();
    SeatDetailViewModel? GetSeatDetail(int seatId);

    // 管理端
    List<AdminSeatItem> GetAllSeats();
    bool CreateSeat(AdminSeatItem model);
    bool UpdateSeat(int id, AdminSeatItem model);
    bool DeleteSeat(int seatId);
}
