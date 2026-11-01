using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieTicketBooking.Models
{
    [Table("ShowSeats")]
    public class ShowSeat
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ShowSeatId { get; set; }

        [Required]
        public int ShowId { get; set; }

        [Required]
        public int SeatId { get; set; }

        [Required]
        [MaxLength(20)]
        public string Status { get; set; } = "Available"; // Available, Booked, Reserved

        // Navigation properties
        [ForeignKey("ShowId")]
        public Show Show { get; set; } = null!;

        [ForeignKey("SeatId")]
        public Seat Seat { get; set; } = null!;

        public ICollection<BookingSeat> BookingSeats { get; set; } = new List<BookingSeat>();
    }
}