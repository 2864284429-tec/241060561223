using System.ComponentModel.DataAnnotations;

namespace LibrarySeatReservation.Web.Models.Entities;

public class Seat
{
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string Location { get; set; } = string.Empty;

    public int Capacity { get; set; } = 1;

    [MaxLength(200)]
    public string? Features { get; set; }

    public bool IsAvailable { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
