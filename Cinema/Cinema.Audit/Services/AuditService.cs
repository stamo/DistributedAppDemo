using Cinema.Audit.Data;
using Cinema.Audit.Data.Models;
using Cinema.Infrastructure.Constants;
using Cinema.Infrastructure.Models;
using Dapr.AppCallback.Autogen.Grpc.v1;
using Dapr.Client;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using System.Text.Json;

namespace Cinema.Audit.Services
{
    public class AuditService(
        IAuditRepository repo,
        ILogger<AuditService> logger,
        DaprClient client) : AppCallback.AppCallbackBase
    {
        readonly JsonSerializerOptions jsonOptions = new JsonSerializerOptions 
        { 
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase 
        };

        public override Task<ListTopicSubscriptionsResponse> ListTopicSubscriptions(Empty request, ServerCallContext context)
        {
            var result = new ListTopicSubscriptionsResponse();
            result.Subscriptions.Add(new TopicSubscription
            {
                PubsubName = PubSubConstants.Name,
                Topic = PubSubConstants.AuditTopic
            });
            
            return Task.FromResult(result);
        }

        public override async Task<TopicEventResponse> OnTopicEvent(TopicEventRequest request, ServerCallContext context)
        {
            if (request.PubsubName == PubSubConstants.Name 
                && request.Topic == PubSubConstants.AuditTopic)
            {
                var input = JsonSerializer.Deserialize<AuditMessage>(request.Data.ToStringUtf8(), this.jsonOptions);

                if (input != null)
                {
                    var state = await client
                        .GetStateAsync<RequestState>(
                            StateConstants.StateStore,
                            input.SessionId);

                    if (state != null)
                    {
                        await client.DeleteStateAsync(
                            StateConstants.StateStore,
                            input.SessionId);

                        AuditLog auditLog = new AuditLog()
                        {
                            ActionName = state.ActionName,
                            ControllerName = state.ControllerName,
                            IPAddress = state.IPAddress,
                            Method = state.Method,
                            TraceId = state.TraceId,
                            Message = input.Message
                        };

                        await repo.AddAsync(auditLog);
                        await repo.SaveChangesAsync();
                    }
                }
            }

            return new TopicEventResponse();
        }
    }
}
