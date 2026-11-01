using System;
using System.ComponentModel.DataAnnotations;

namespace MovieTicketBooking.DTOs.Shows
{
    public class UpdateShowRequest
    {
        [Required(ErrorMessage = "Movie ID is required.")]
        public int MovieId { get; set; }

        [Required(ErrorMessage = "Screen ID is required.")]
        public int ScreenId { get; set; }

        [Required(ErrorMessage = "Start time is required.")]
        public DateTime StartTime { get; set; }

        [Required(ErrorMessage = "End time is required.")]
        public DateTime EndTime { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, 10000.00, ErrorMessage = "Price must be greater than zero.")]
        public decimal Price { get; set; }
    }
}