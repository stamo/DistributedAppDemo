namespace Cinema.ApiGateway.Models
{
    public class HallInfoModel
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public int Seats { get; set; }
    }
}