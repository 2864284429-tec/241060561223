using LibrarySeatReservation.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySeatReservation.Web.Controllers;

public class SeatController : Controller
{
    private readonly ISeatService _seatService;

    public SeatController(ISeatService seatService)
    {
        _seatService = seatService;
    }

    /// <summary>
    /// 座位列表页 - GET /Seat/List
    /// 查询所有座位及当前时段预约状态
    /// </summary>
    public IActionResult List()
    {
        var seats = _seatService.GetAllSeatsWithStatus();
        return View(seats);
    }

    /// <summary>
    /// 座位详情页 - GET /Seat/Detail/{id}
    /// 查询单个座位详情
    /// </summary>
    public IActionResult Detail(int id)
    {
        var seat = _seatService.GetSeatDetail(id);
        if (seat == null)
        {
            return NotFound();
        }
        return View(seat);
    }
}
