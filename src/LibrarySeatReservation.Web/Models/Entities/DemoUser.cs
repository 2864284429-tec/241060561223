using System.ComponentModel.DataAnnotations;

namespace LibrarySeatReservation.Web.Models.Entities;

public class DemoUser
{
    [Key]
    [MaxLength(50)]
    public string Id { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
