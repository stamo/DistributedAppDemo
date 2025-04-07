using Cinema.Infrastructure.Constants;
using Dapr.Client;
using GrpcServices.HallManager;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.ApiGateway.Controllers
{
    [ApiController]
    public class HallsController(DaprClient client) : Controller
    {
        [HttpPost("createCinema")]
        public async Task<IActionResult> CreateCinema(string name, string location)
        {
            var responce = await client
                .InvokeMethodGrpcAsync<CreateCinemaRequest, CreateCinemaReply>(
                    HallsServiceConstants.AppId,
                    HallsServiceConstants.CreateCinema,
                    new CreateCinemaRequest() 
                    { 
                        Name = name, 
                        Location = location 
                    }
                );

            if (responce.Result.Code == GrpcServices.Common.ResultCodes.Ok)
            {
                return Ok(responce.CinemaId);
            }

            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }
}
