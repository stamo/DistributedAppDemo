using System.Net;

namespace Cinema.Infrastructure.Models
{
    public class RequestState
    {
        public required string TraceId { get; set; }

        public required string ControllerName { get; set; }

        public required string ActionName { get; set; }

        public required string Method { get; set; }

        public string? IPAddress { get; set; }
    }
}
