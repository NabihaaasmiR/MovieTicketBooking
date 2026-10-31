using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieTicketBooking.Models
{
    [Table("Movies")]
    public class Movie
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MovieId { get; set; }

        [Required]
        [MaxLength(150)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Description { get; set; }

        [Required]
        public int DurationMinutes { get; set; }

        [Required]
        public DateTime ReleaseDate { get; set; }

        [Required]
        [MaxLength(50)]
        public string Language { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Genre { get; set; } = string.Empty;

        // Temporarily comment this out until Phase 6 (Shows module):
        // public ICollection<Show> Shows { get; set; } = new List<Show>();
    }
}