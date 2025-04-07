using Microsoft.EntityFrameworkCore;

namespace Cinema.HallManager.Data
{
    public class HallsDbContext : DbContext
    {
        public HallsDbContext(DbContextOptions<HallsDbContext> options)
            : base(options)
        {
            
        }
    }
}
