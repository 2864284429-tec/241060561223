using LibrarySeatReservation.Web.Models.ViewModels.Admin;
using LibrarySeatReservation.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySeatReservation.Web.Controllers.Admin;

/// <summary>
/// 管理端 - 座位管理控制器
/// 路由：/Admin/SeatList (GET/POST)
/// 权限：需管理员登录（Session IsAdminLoggedIn）
/// </summary>
public class AdminSeatController : Controller
{
    private readonly ISeatService _seatService;

    public AdminSeatController(ISeatService seatService)
    {
        _seatService = seatService;
    }

    /// <summary>
    /// 座位列表页 - GET /Admin/SeatList
    /// </summary>
    public IActionResult SeatList()
    {
        if (!AdminAuthController.IsAdminLoggedIn(HttpContext))
            return RedirectToAction("Login", "AdminAuth");

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
        if (!AdminAuthController.IsAdminLoggedIn(HttpContext))
            return RedirectToAction("Login", "AdminAuth");

        if (string.IsNullOrWhiteSpace(model.Name) || string.IsNullOrWhiteSpace(model.Location))
        {
            TempData["ErrorMessage"] = "座位名称和位置不能为空";
            return RedirectToAction("SeatList");
        }

        if (model.Capacity < 1)
            model.Capacity = 1;

        var success = _seatService.CreateSeat(model);
        TempData[success ? "SuccessMessage" : "ErrorMessage"] =
            success ? "座位新增成功" : "座位新增失败，请重试";

        return RedirectToAction("SeatList");
    }

    /// <summary>
    /// 编辑座位 - POST /Admin/SeatList/Edit/{id}
    /// </summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, AdminSeatItem model)
    {
        if (!AdminAuthController.IsAdminLoggedIn(HttpContext))
            return RedirectToAction("Login", "AdminAuth");

        if (string.IsNullOrWhiteSpace(model.Name) || string.IsNullOrWhiteSpace(model.Location))
        {
            TempData["ErrorMessage"] = "座位名称和位置不能为空";
            return RedirectToAction("SeatList");
        }

        if (model.Capacity < 1)
            model.Capacity = 1;

        var success = _seatService.UpdateSeat(id, model);
        TempData[success ? "SuccessMessage" : "ErrorMessage"] =
            success ? "座位信息已更新" : "更新失败，座位可能不存在";

        return RedirectToAction("SeatList");
    }

    /// <summary>
    /// 删除座位 - POST /Admin/SeatList/Delete/{id}
    /// </summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
        if (!AdminAuthController.IsAdminLoggedIn(HttpContext))
            return RedirectToAction("Login", "AdminAuth");

        var success = _seatService.DeleteSeat(id);
        TempData[success ? "SuccessMessage" : "ErrorMessage"] =
            success ? "座位已删除" : "删除失败，该座位可能有未完成的预约";

        return RedirectToAction("SeatList");
    }
}
