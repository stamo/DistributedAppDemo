using Dapr.AppCallback.Autogen.Grpc.v1;
using Dapr.Client;

namespace Cinema.Audit.Services
{
    public class AuditService(
        ILogger<AuditService> logger,
        DaprClient client) : AppCallback.AppCallbackBase
    {
        
    }
}
