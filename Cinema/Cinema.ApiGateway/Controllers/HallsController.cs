using Cinema.ApiGateway.Models;
using Cinema.Infrastructure.Constants;
using Cinema.Infrastructure.Models;
using Dapr.Client;
using GrpcServices.HallManager;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.ApiGateway.Controllers
{
    
    public class HallsController() : BaseController
    {
        [HttpPost("createCinema")]
        public async Task<IActionResult> CreateCinema(CreateCinemaModel model)
        {
            var responce = await client
                .InvokeMethodGrpcAsync<CreateCinemaRequest, CreateReply>(
                    HallsServiceConstants.AppId,
                    HallsServiceConstants.CreateCinema,
                    new CreateCinemaRequest() 
                    { 
                        Name = model.Name, 
                        Location = model.Location,
                        SessionId = sessionId
                    }
                );

            if (responce.Result.Code == GrpcServices.Common.ResultCodes.Ok)
            {
                return Ok(responce.Id);
            }

            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }

        [HttpPost("createHall")]
        public async Task<IActionResult> CreateHall(CreateHallModel model)
        {
            var responce = await client
                .InvokeMethodGrpcAsync<CreateHallRequest, CreateReply>(
                    HallsServiceConstants.AppId,
                    HallsServiceConstants.CreateHall,
                    new CreateHallRequest()
                    {
                        Name = model.Name,
                        Seats = model.Seats,
                        CinemaId = model.CinemaId,
                        SessionId = sessionId
                    }
                );

            if (responce.Result.Code == GrpcServices.Common.ResultCodes.Ok)
            {
                return Ok(responce.Id);
            }

            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }

        [HttpGet("GetCinema")]
        public async Task<IActionResult> GetCinema(int id)
        {
            var responce = await client
                .InvokeMethodGrpcAsync<GetCinemaRequest, GetCinemaReply>(
                    HallsServiceConstants.AppId,
                    HallsServiceConstants.GetCinema,
                    new GetCinemaRequest()
                    {
                        CinemaId = id,
                        SessionId = sessionId
                    }
                );

            if (responce.Result.Code == GrpcServices.Common.ResultCodes.Ok)
            {
                var result = new CinemaInfoModel()
                {
                    Id = responce.Id,
                    Name = responce.Name,
                    Location = responce.Location,
                    Halls = responce.Halls
                        .Select(h => new HallInfoModel()
                        {
                            Id = h.Id,
                            Name = h.Name,
                            Seats = h.Seats
                        })
                        .ToList()
                };

                return Ok(result);
            }

            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }
}
