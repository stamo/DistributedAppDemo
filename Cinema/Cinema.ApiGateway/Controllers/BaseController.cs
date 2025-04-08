using Cinema.Infrastructure.Constants;
using Cinema.Infrastructure.Models;
using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Cinema.ApiGateway.Controllers
{
    [ApiController]
    public class BaseController : Controller
    {
        protected string sessionId = null!;
        private DaprClient? _client;

        protected DaprClient client 
        {
            get 
            {
                if (_client == null)
                {
                    _client = this.HttpContext
                        .RequestServices
                        .GetRequiredService<DaprClient>();
                }
                
                return _client;
            } 
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var requestState = new RequestState()
            {
                ActionName = this.ControllerContext
                    .ActionDescriptor
                    .ActionName,
                ControllerName = this.ControllerContext
                    .ActionDescriptor
                    .ControllerName,
                Method = this.HttpContext.Request.Method,
                TraceId = this.HttpContext.TraceIdentifier,
                IPAddress = this.HttpContext.Connection.RemoteIpAddress?.ToString()
            };

            sessionId = Guid.NewGuid().ToString();

            await client.SaveStateAsync(StateConstants.StateStore, sessionId, requestState);
            await base.OnActionExecutionAsync(context, next);
        }
    }
}
