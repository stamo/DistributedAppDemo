namespace Cinema.ApiGateway.Models
{
    public class CreateHallModel
    {
        public required string Name { get; set; }

        public int Seats { get; set; }

        public int CinemaId { get; set; }
    }
}
