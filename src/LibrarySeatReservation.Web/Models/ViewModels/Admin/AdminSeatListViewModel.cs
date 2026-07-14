namespace LibrarySeatReservation.Web.Models.ViewModels.Admin;

public class AdminSeatListViewModel
{
    public List<AdminSeatItem> Seats { get; set; } = new();
}

public class AdminSeatItem
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public int Capacity { get; set; }
    public string? Features { get; set; }
    public bool IsAvailable { get; set; }
}
