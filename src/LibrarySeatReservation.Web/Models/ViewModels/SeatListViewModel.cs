namespace LibrarySeatReservation.Web.Models.ViewModels;

public class SeatListItem
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string? Features { get; set; }
    public bool IsAvailable { get; set; }
    /// <summary>
    /// 当前时段状态：可用 / 已预约 / 不可用
    /// </summary>
    public string CurrentStatus { get; set; } = "可用";
}
