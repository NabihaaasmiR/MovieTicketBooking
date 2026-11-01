using System.ComponentModel.DataAnnotations;

namespace MovieTicketBooking.DTOs.Theatres
{
    public class CreateTheatreRequest
    {
        [Required(ErrorMessage = "Theatre name is required.")]
        [MaxLength(150)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Location is required.")]
        [MaxLength(200)]
        public string Location { get; set; } = string.Empty;
    }
}