using Cinema.Audit.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Audit.Data
{
    public class AuditDbContext : DbContext
    {
        public AuditDbContext(DbContextOptions<AuditDbContext> options)
            : base(options)
        {
        }

        public DbSet<AuditLog>  AuditLog { get; set; }
    }
}
