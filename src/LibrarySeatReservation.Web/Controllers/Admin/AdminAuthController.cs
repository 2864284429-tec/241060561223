using LibrarySeatReservation.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySeatReservation.Web.Controllers.Admin;

/// <summary>
/// 管理员认证控制器
/// 路由：/Admin/Login (GET/POST)
/// Session Key：IsAdminLoggedIn
/// </summary>
public class AdminAuthController : Controller
{
    private readonly IAdminAuthService _adminAuthService;

    public AdminAuthController(IAdminAuthService adminAuthService)
    {
        _adminAuthService = adminAuthService;
    }

    /// <summary>
    /// 登录页面 - GET /Admin/Login
    /// </summary>
    [HttpGet]
    public IActionResult Login()
    {
        // 已登录则跳转预约管理页
        if (HttpContext.Session.GetString("IsAdminLoggedIn") == "true")
        {
            return RedirectToAction("ReservationList", "AdminHome");
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

        // 登录成功，设置 Session
        HttpContext.Session.SetString("IsAdminLoggedIn", "true");
        HttpContext.Session.SetString("AdminUsername", username);

        return RedirectToAction("ReservationList", "AdminHome");
    }

    /// <summary>
    /// 退出登录 - POST /Admin/Logout
    /// </summary>
    [HttpPost]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }

    /// <summary>
    /// 检查管理员是否已登录的辅助方法
    /// </summary>
    public static bool IsAdminLoggedIn(HttpContext httpContext)
    {
        return httpContext.Session.GetString("IsAdminLoggedIn") == "true";
    }
}
