using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cinema.HallManager.Data.Models
{
    [Comment("Theatre halls")]
    public class Hall
    {
        [Key]
        [Comment("Record identifier")]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        [Comment("Hall name")]
        public required string Name { get; set; }

        [Required]
        [Comment("Number of seats in the hall")]
        public int Seats { get; set; }

        [Required]
        [Comment("Identifier of the cinema")]
        public int CinemaId { get; set; }

        [ForeignKey(nameof(CinemaId))]
        public CinemaTheatre Cinema { get; set; } = null!;
    }
}
