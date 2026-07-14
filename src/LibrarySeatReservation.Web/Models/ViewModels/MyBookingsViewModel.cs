namespace LibrarySeatReservation.Web.Models.ViewModels;

public class MyBookingsViewModel
{
    public string CurrentUserName { get; set; } = string.Empty;
    public List<BookingItem> Bookings { get; set; } = new();
}

public class BookingItem
{
    public int ReservationId { get; set; }
    public string SeatName { get; set; } = string.Empty;
    public string SeatLocation { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public int TimeSlot { get; set; }
    public string Status { get; set; } = string.Empty;
    /// <summary>
    /// 是否可取消（未过开始时间且状态为"已预约"）
    /// </summary>
    public bool CanCancel { get; set; }
}
