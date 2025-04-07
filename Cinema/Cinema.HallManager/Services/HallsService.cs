using Cinema.HallManager.Data;
using Cinema.HallManager.Data.Models;
using Cinema.Infrastructure.Constants;
using Dapr.AppCallback.Autogen.Grpc.v1;
using Dapr.Client.Autogen.Grpc.v1;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcServices.HallManager;

namespace Cinema.HallManager.Services
{
    public class HallsService(
        IHallsRepository repo,
        ILogger<HallsService> logger) : AppCallback.AppCallbackBase
    {
        public override async Task<InvokeResponse> OnInvoke(InvokeRequest request, ServerCallContext context)
        {
            var response = new InvokeResponse();

            switch (request.Method)
            {
                case HallsServiceConstants.CreateCinema:
                    response.Data = await CreateCinema(request); 
                    break;
                case "CreateHall":
                    response.Data = await CreateHall(request);
                    break;
                case "GetCinema":
                    response.Data = await GetCinema(request);
                    break;
                default:
                    throw new InvalidOperationException("Unknown method");
            }

            return response;
        }

        private async Task<Any> GetCinema(InvokeRequest request)
        {
            throw new NotImplementedException();
        }

        private async Task<Any> CreateHall(InvokeRequest request)
        {
            throw new NotImplementedException();
        }

        private async Task<Any> CreateCinema(InvokeRequest request)
        {
            CreateCinemaReply reply;

            try
            {
                CreateCinemaRequest cinemaData = request.Data.Unpack<CreateCinemaRequest>();

                CinemaTheatre cinema = new CinemaTheatre()
                {
                    Name = cinemaData.Name,
                    City = cinemaData.Location
                };

                await repo.AddAsync(cinema);
                await repo.SaveChangesAsync();
                reply = new CreateCinemaReply()
                {
                    CinemaId = cinema.Id,
                    Result = new GrpcServices.Common.ResultStatus()
                    {
                        Code = GrpcServices.Common.ResultCodes.Ok
                    }
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "CreateCinema");
                reply = new CreateCinemaReply()
                {
                    CinemaId = 0,
                    Result = new GrpcServices.Common.ResultStatus()
                    {
                        Code = GrpcServices.Common.ResultCodes.InternalServerError,
                        Message = ex.Message
                    }
                };
            }

            return Any.Pack(reply);
        }
    }
}
