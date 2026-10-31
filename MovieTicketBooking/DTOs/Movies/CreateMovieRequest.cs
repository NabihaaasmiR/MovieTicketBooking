using System;
using System.ComponentModel.DataAnnotations;

namespace MovieTicketBooking.DTOs.Movies
{
    public class CreateMovieRequest
    {
        [Required(ErrorMessage = "Title is required.")]
        [MaxLength(150)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Duration is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Duration must be greater than zero.")]
        public int DurationMinutes { get; set; }

        [Required(ErrorMessage = "Release date is required.")]
        public DateTime ReleaseDate { get; set; }

        [Required(ErrorMessage = "Language is required.")]
        [MaxLength(50)]
        public string Language { get; set; } = string.Empty;

        [Required(ErrorMessage = "Genre is required.")]
        [MaxLength(50)]
        public string Genre { get; set; } = string.Empty;
    }
}