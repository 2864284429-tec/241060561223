namespace LibrarySeatReservation.Web.Models.ViewModels.Admin;

public class AdminReservationListViewModel
{
    public List<AdminBookingItem> Bookings { get; set; } = new();
}

public class AdminBookingItem
{
    public int ReservationId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string SeatName { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public int TimeSlot { get; set; }
    public string Status { get; set; } = string.Empty;
}
