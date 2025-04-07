using Cinema.Infrastructure.Data;

namespace Cinema.HallManager.Data
{
    public class HallsRepository : Repository, IHallsRepository
    {
        public HallsRepository(HallsDbContext context) : base(context)
        {
        }
    }
}
