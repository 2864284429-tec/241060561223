namespace LibrarySeatReservation.Web.Models.ViewModels;

public class SeatDetailViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public int Capacity { get; set; }
    public string? Features { get; set; }
    public bool IsAvailable { get; set; }
    public bool CanReserve { get; set; }
}
