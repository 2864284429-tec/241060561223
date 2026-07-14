using LibrarySeatReservation.Web.Models.ViewModels.Admin;
using LibrarySeatReservation.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySeatReservation.Web.Controllers;

/// <summary>
/// 管理端统一控制器
/// 路由：/Admin/Login, /Admin/ReservationList, /Admin/SeatList
/// Session Key：IsAdminLoggedIn
/// </summary>
public class AdminController : Controller
{
    private readonly IAdminAuthService _adminAuthService;
    private readonly IReservationService _reservationService;
    private readonly ISeatService _seatService;

    public AdminController(
        IAdminAuthService adminAuthService,
        IReservationService reservationService,
        ISeatService seatService)
    {
        _adminAuthService = adminAuthService;
        _reservationService = reservationService;
        _seatService = seatService;
    }

    #region 认证 (原 AdminAuthController)

    /// <summary>
    /// 登录页面 - GET /Admin/Login
    /// </summary>
    [HttpGet]
    public IActionResult Login()
    {
        if (HttpContext.Session.GetString("IsAdminLoggedIn") == "true")
        {
            return RedirectToAction(nameof(ReservationList));
        }
        return View();
    }

    /// <summary>
    /// 登录提交 - POST /Admin/Login
    /// 校验硬编码凭据 admin/123456，成功后设置 Session 并跳转
    /// </summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Login(string username, string password)
    {
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            ViewBag.ErrorMessage = "请输入用户名和密码";
            return View();
        }

        if (!_adminAuthService.ValidateCredentials(username, password))
        {
            ViewBag.ErrorMessage = "账号或密码错误";
            return View();
        }

        HttpContext.Session.SetString("IsAdminLoggedIn", "true");
        HttpContext.Session.SetString("AdminUsername", username);

        return RedirectToAction(nameof(ReservationList));
    }

    /// <summary>
    /// 退出登录 - POST /Admin/Logout
    /// </summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction(nameof(Login));
    }

    /// <summary>
    /// 检查管理员是否已登录的辅助方法
    /// </summary>
    public static bool IsAdminLoggedIn(HttpContext httpContext)
    {
        return httpContext.Session.GetString("IsAdminLoggedIn") == "true";
    }

    #endregion

    #region 预约管理 (原 AdminHomeController)

    /// <summary>
    /// 预约管理页 - GET /Admin/ReservationList
    /// 权限：需管理员登录
    /// </summary>
    public IActionResult ReservationList()
    {
        if (!IsAdminLoggedIn(HttpContext))
        {
            return RedirectToAction(nameof(Login));
        }

        var bookings = _reservationService.GetAllBookings();
        var viewModel = new AdminReservationListViewModel
        {
            Bookings = bookings
        };
        return View(viewModel);
    }

    #endregion

    #region 座位管理 (原 AdminSeatController)

    /// <summary>
    /// 座位列表页 - GET /Admin/SeatList
    /// </summary>
    public IActionResult SeatList()
    {
        if (!IsAdminLoggedIn(HttpContext))
            return RedirectToAction(nameof(Login));

        var seats = _seatService.GetAllSeats();
        var viewModel = new AdminSeatListViewModel { Seats = seats };
        return View(viewModel);
    }

    /// <summary>
    /// 新增座位 - POST /Admin/SeatList/Create
    /// </summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(AdminSeatItem model)
    {
        if (!IsAdminLoggedIn(HttpContext))
            return RedirectToAction(nameof(Login));

        if (string.IsNullOrWhiteSpace(model.Name) || string.IsNullOrWhiteSpace(model.Location))
        {
            TempData["ErrorMessage"] = "座位名称和位置不能为空";
            return RedirectToAction(nameof(SeatList));
        }

        if (model.Capacity < 1)
            model.Capacity = 1;

        var success = _seatService.CreateSeat(model);
        TempData[success ? "SuccessMessage" : "ErrorMessage"] =
            success ? "座位新增成功" : "座位新增失败，请重试";

        return RedirectToAction(nameof(SeatList));
    }

    /// <summary>
    /// 编辑座位 - POST /Admin/SeatList/Edit/{id}
    /// </summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, AdminSeatItem model)
    {
        if (!IsAdminLoggedIn(HttpContext))
            return RedirectToAction(nameof(Login));

        if (string.IsNullOrWhiteSpace(model.Name) || string.IsNullOrWhiteSpace(model.Location))
        {
            TempData["ErrorMessage"] = "座位名称和位置不能为空";
            return RedirectToAction(nameof(SeatList));
        }

        if (model.Capacity < 1)
            model.Capacity = 1;

        var success = _seatService.UpdateSeat(id, model);
        TempData[success ? "SuccessMessage" : "ErrorMessage"] =
            success ? "座位信息已更新" : "更新失败，座位可能不存在";

        return RedirectToAction(nameof(SeatList));
    }

    /// <summary>
    /// 删除座位 - POST /Admin/SeatList/Delete/{id}
    /// </summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
        if (!IsAdminLoggedIn(HttpContext))
            return RedirectToAction(nameof(Login));

        var success = _seatService.DeleteSeat(id);
        TempData[success ? "SuccessMessage" : "ErrorMessage"] =
            success ? "座位已删除" : "删除失败，该座位可能有未完成的预约";

        return RedirectToAction(nameof(SeatList));
    }

    #endregion
}
