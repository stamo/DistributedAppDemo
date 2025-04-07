using Dapr.AppCallback.Autogen.Grpc.v1;

namespace Cinema.HallManager.Services
{
    public class HallsService : AppCallback.AppCallbackBase
    {
        private readonly ILogger logger;

        public HallsService(ILogger<HallsService> _logger)
        {
            logger = _logger;
        }
    }
}
