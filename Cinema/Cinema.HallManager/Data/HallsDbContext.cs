using Cinema.HallManager.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Cinema.HallManager.Data
{
    public class HallsDbContext : DbContext
    {
        public HallsDbContext(DbContextOptions<HallsDbContext> options)
            : base(options)
        {
            
        }

        public DbSet<CinemaTheatre> Cinemas { get; set; }

        public DbSet<Hall> Halls { get; set; }
    }
}
