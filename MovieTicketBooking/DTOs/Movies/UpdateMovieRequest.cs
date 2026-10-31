using System;
using System.ComponentModel.DataAnnotations;

namespace MovieTicketBooking.DTOs.Movies
{
    public class UpdateMovieRequest
    {
        [Required]
        [MaxLength(150)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Description { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Duration must be greater than zero.")]
        public int DurationMinutes { get; set; }

        [Required]
        public DateTime ReleaseDate { get; set; }

        [Required]
        [MaxLength(50)]
        public string Language { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Genre { get; set; } = string.Empty;
    }
}