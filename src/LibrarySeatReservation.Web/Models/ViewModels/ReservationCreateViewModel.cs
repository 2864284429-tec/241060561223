namespace LibrarySeatReservation.Web.Models.ViewModels;

public class ReservationCreateViewModel
{
    public int SeatId { get; set; }
    public string SeatName { get; set; } = string.Empty;
    public string SeatLocation { get; set; } = string.Empty;
    public DateTime SelectedDate { get; set; }
    public int SelectedTimeSlot { get; set; }

    /// <summary>
    /// 可选日期列表（今天、明天、后天）
    /// </summary>
    public List<DateTime> AvailableDates { get; set; } = new();

    /// <summary>
    /// 可选时段列表（8-20），已过时段标记为不可选
    /// </summary>
    public List<TimeSlotOption> AvailableTimeSlots { get; set; } = new();
}

public class TimeSlotOption
{
    public int Slot { get; set; }
    public string Label { get; set; } = string.Empty;
    public bool IsAvailable { get; set; }
}
