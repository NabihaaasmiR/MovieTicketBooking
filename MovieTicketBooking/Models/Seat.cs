using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieTicketBooking.Models
{
    [Table("Seats")]
    public class Seat
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SeatId { get; set; }

        [Required]
        public int ScreenId { get; set; }

        [Required]
        [MaxLength(20)]
        public string SeatNumber { get; set; } = string.Empty; // e.g. "A1", "B12"

        [Required]
        [MaxLength(50)]
        public string SeatType { get; set; } = "Regular"; // e.g., "Regular", "Premium", "VIP"

        // Navigation property back to Screen (One-to-Many)
        [ForeignKey("ScreenId")]
        public Screen Screen { get; set; } = null!;
    }
}