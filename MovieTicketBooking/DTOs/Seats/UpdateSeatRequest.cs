using System.ComponentModel.DataAnnotations;

namespace MovieTicketBooking.DTOs.Seats
{
    public class UpdateSeatRequest
    {
        [Required(ErrorMessage = "Screen ID is required.")]
        public int ScreenId { get; set; }

        [Required(ErrorMessage = "Seat number is required.")]
        [MaxLength(20)]
        public string SeatNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Seat type is required.")]
        [MaxLength(50)]
        public string SeatType { get; set; } = "Regular";
    }
}