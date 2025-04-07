using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Cinema.HallManager.Data.Models
{
    [Comment("Cinema Theatres")]
    public class CinemaTheatre
    {
        [Key]
        [Comment("Record identifier")]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        [Comment("Cinema name")]
        public required string Name { get; set; }

        [Required]
        [MaxLength(100)]
        [Comment("Cinema location")]
        public required string City { get; set; }

        public List<Hall> Halls { get; set; } = new List<Hall>();
    }
}
