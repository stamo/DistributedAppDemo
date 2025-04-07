using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Cinema.HallManager.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<HallsDbContext>
    {
        public HallsDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddUserSecrets<Program>()
                .Build();

            var connectionString = configuration.GetConnectionString("HallsDbConnectionConnection");

            var builder = new DbContextOptionsBuilder<HallsDbContext>();

            builder.UseNpgsql(connectionString)
                .UseSnakeCaseNamingConvention();
            
            return new HallsDbContext(builder.Options);
        }
    }
}
