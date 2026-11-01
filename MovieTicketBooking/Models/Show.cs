using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieTicketBooking.Models
{
    [Table("Shows")]
    public class Show
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ShowId { get; set; }

        [Required]
        public int MovieId { get; set; }

        [Required]
        public int ScreenId { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        // Navigation properties
        [ForeignKey("MovieId")]
        public Movie Movie { get; set; } = null!;

        [ForeignKey("ScreenId")]
        public Screen Screen { get; set; } = null!;
    }
}