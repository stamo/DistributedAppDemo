﻿using Cinema.HallManager.Data;
using Cinema.HallManager.Data.Models;
using Cinema.Infrastructure.Constants;
using Cinema.Infrastructure.Models;
using Dapr.AppCallback.Autogen.Grpc.v1;
using Dapr.Client;
using Dapr.Client.Autogen.Grpc.v1;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcServices.HallManager;
using Microsoft.EntityFrameworkCore;

namespace Cinema.HallManager.Services
{
    public class HallsService(
        IHallsRepository repo,
        ILogger<HallsService> logger,
        DaprClient client) : AppCallback.AppCallbackBase
    {
        public override async Task<InvokeResponse> OnInvoke(InvokeRequest request, ServerCallContext context)
        {
            var response = new InvokeResponse();

            switch (request.Method)
            {
                case HallsServiceConstants.CreateCinema:
                    response.Data = await CreateCinema(request); 
                    break;
                case HallsServiceConstants.CreateHall:
                    response.Data = await CreateHall(request);
                    break;
                case HallsServiceConstants.GetCinema:
                    response.Data = await GetCinema(request);
                    break;
                default:
                    throw new InvalidOperationException("Unknown method");
            }

            return response;
        }

        private async Task<Any> GetCinema(InvokeRequest request)
        {
            GetCinemaReply reply;
            GetCinemaRequest cinemaData = request.Data.Unpack<GetCinemaRequest>();

            try
            {
                var cinema = await repo.AllReadonly<CinemaTheatre>()
                    .Where(c => c.Id == cinemaData.CinemaId)
                    .Include(c => c.Halls)
                    .FirstOrDefaultAsync();

                if (cinema != null)
                {
                    reply = new GetCinemaReply()
                    {
                        Result = new GrpcServices.Common.ResultStatus()
                        {
                            Code = GrpcServices.Common.ResultCodes.Ok
                        },
                        Id = cinema.Id,
                        Location = cinema.City,
                        Name = cinema.Name
                    };

                    reply.Halls.AddRange(cinema.Halls
                        .Select(h => new HallInfo()
                        {
                            Id = h.Id,
                            Name = h.Name,
                            Seats = h.Seats
                        }));
                }
                else
                {
                    reply = new GetCinemaReply()
                    {
                        Result = new GrpcServices.Common.ResultStatus()
                        {
                            Code = GrpcServices.Common.ResultCodes.NotFound
                        }
                    };
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "CreateHall");
                reply = new GetCinemaReply()
                {
                    Result = new GrpcServices.Common.ResultStatus()
                    {
                        Code = GrpcServices.Common.ResultCodes.InternalServerError,
                        Message = ex.Message
                    }
                };
            }

            AuditMessage message = new AuditMessage()
            {
                SessionId = cinemaData.SessionId,
                Message = $"Get information for cinema with Id {cinemaData.CinemaId}",
                ResultCode = (int)reply.Result.Code,
            };

            await client.PublishEventAsync(
                PubSubConstants.Name,
                PubSubConstants.AuditTopic,
                message);

            return Any.Pack(reply);
        }

        private async Task<Any> CreateHall(InvokeRequest request)
        {
            CreateReply reply;
            CreateHallRequest hallData = request.Data.Unpack<CreateHallRequest>();

            try
            {
                Hall hall = new Hall()
                { 
                    Name = hallData.Name,
                    Seats = hallData.Seats,
                    CinemaId = hallData.CinemaId,
                };

                await repo.AddAsync(hall);
                await repo.SaveChangesAsync();
                reply = new CreateReply()
                {
                    Id = hall.Id,
                    Result = new GrpcServices.Common.ResultStatus()
                    {
                        Code = GrpcServices.Common.ResultCodes.Ok
                    }
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "CreateHall");
                reply = new CreateReply()
                {
                    Id = 0,
                    Result = new GrpcServices.Common.ResultStatus()
                    {
                        Code = GrpcServices.Common.ResultCodes.InternalServerError,
                        Message = ex.Message
                    }
                };
            }

            AuditMessage message = new AuditMessage()
            {
                SessionId = hallData.SessionId,
                Message = $"New hall {hallData.Name} created in cinema with Id {hallData.CinemaId}",
                ResultCode = (int)reply.Result.Code,
            };

            await client.PublishEventAsync(
                PubSubConstants.Name,
                PubSubConstants.AuditTopic,
                message);

            return Any.Pack(reply);
        }

        private async Task<Any> CreateCinema(InvokeRequest request)
        {
            CreateReply reply;
            CreateCinemaRequest cinemaData = request.Data.Unpack<CreateCinemaRequest>();

            try
            {
                CinemaTheatre cinema = new CinemaTheatre()
                {
                    Name = cinemaData.Name,
                    City = cinemaData.Location
                };

                await repo.AddAsync(cinema);
                await repo.SaveChangesAsync();
                reply = new CreateReply()
                {
                    Id = cinema.Id,
                    Result = new GrpcServices.Common.ResultStatus()
                    {
                        Code = GrpcServices.Common.ResultCodes.Ok
                    }
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "CreateCinema");
                reply = new CreateReply()
                {
                    Id = 0,
                    Result = new GrpcServices.Common.ResultStatus()
                    {
                        Code = GrpcServices.Common.ResultCodes.InternalServerError,
                        Message = ex.Message
                    }
                };
            }

            AuditMessage message = new AuditMessage()
            {
                SessionId = cinemaData.SessionId,
                Message = $"Cinema {cinemaData.Name} was created in {cinemaData.Location}",
                ResultCode = (int)reply.Result.Code,
            };

            await client.PublishEventAsync(
                PubSubConstants.Name,
                PubSubConstants.AuditTopic,
                message);

            return Any.Pack(reply);
        }
    }
}
