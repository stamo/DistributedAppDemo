using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cinema.Audit.Data.Models
{
    public class AuditLog
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        public required string TraceId { get; set; }

        [Required]
        [MaxLength(100)]
        public required string ControllerName { get; set; }

        [Required]
        [MaxLength(100)]
        public required string ActionName { get; set; }

        [Required]
        [MaxLength(20)]
        public required string Method { get; set; }

        [MaxLength(20)]
        public string? IPAddress { get; set; }

        [Required]
        [MaxLength(500)]
        public required string Message { get; set; }

        [Required]
        public int ResultCode { get; set; }

        [Column(TypeName = "timestamptz")]
        public DateTime Created { get; set; }
    }
}
