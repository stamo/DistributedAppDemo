using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Cinema.Audit.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AuditDbContext>
    {
        public AuditDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddUserSecrets<Program>()
                .Build();

            var connectionString = configuration.GetConnectionString("AuditDbConnection");

            var builder = new DbContextOptionsBuilder<AuditDbContext>();

            builder.UseNpgsql(connectionString)
                .UseSnakeCaseNamingConvention();
            
            return new AuditDbContext(builder.Options);
        }
    }
}
