using Cinema.Infrastructure.Data;

namespace Cinema.Audit.Data
{
    public class AuditRepository : Repository, IAuditRepository
    {
        public AuditRepository(AuditDbContext context) : base(context)
        {
        }
    }
}
