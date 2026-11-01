using System.ComponentModel.DataAnnotations;

namespace MovieTicketBooking.DTOs.Screens
{
    public class CreateScreenRequest
    {
        [Required(ErrorMessage = "Theatre ID is required.")]
        public int TheatreId { get; set; }

        [Required(ErrorMessage = "Screen name is required.")]
        [MaxLength(50)]
        public string ScreenName { get; set; } = string.Empty;
    }
}