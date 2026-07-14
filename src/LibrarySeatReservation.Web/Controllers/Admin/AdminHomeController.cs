using LibrarySeatReservation.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySeatReservation.Web.Controllers.Admin;

/// <summary>
/// 管理端 - 预约管理控制器
/// 路由：/Admin/ReservationList (GET)
/// </summary>
public class AdminHomeController : Controller
{
    private readonly IReservationService _reservationService;

    public AdminHomeController(IReservationService reservationService)
    {
        _reservationService = reservationService;
    }

    /// <summary>
    /// 预约管理页 - GET /Admin/ReservationList
    /// 权限：需管理员登录
    /// </summary>
    public IActionResult ReservationList()
    {
        if (!AdminAuthController.IsAdminLoggedIn(HttpContext))
        {
            return RedirectToAction("Login", "AdminAuth");
        }

        var bookings = _reservationService.GetAllBookings();
        var viewModel = new Models.ViewModels.Admin.AdminReservationListViewModel
        {
            Bookings = bookings
        };
        return View(viewModel);
    }
}
