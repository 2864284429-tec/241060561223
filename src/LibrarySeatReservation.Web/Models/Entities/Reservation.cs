using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibrarySeatReservation.Web.Models.Entities;

public class Reservation
{
    public int Id { get; set; }

    public int SeatId { get; set; }

    [Required]
    [MaxLength(50)]
    public string UserId { get; set; } = string.Empty;

    public DateTime Date { get; set; }

    /// <summary>
    /// 时段编号：8-20，对应 8:00-21:00
    /// </summary>
    public int TimeSlot { get; set; }

    /// <summary>
    /// 已预约 / 已取消 / 已过时
    /// </summary>
    [Required]
    [MaxLength(20)]
    public string Status { get; set; } = "已预约";

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    [ForeignKey(nameof(SeatId))]
    public Seat? Seat { get; set; }

    [ForeignKey(nameof(UserId))]
    public DemoUser? DemoUser { get; set; }
}
