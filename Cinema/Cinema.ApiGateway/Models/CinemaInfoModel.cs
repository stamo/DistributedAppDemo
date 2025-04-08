namespace Cinema.ApiGateway.Models
{
    public class CinemaInfoModel
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public required string Location { get; set; }

        public List<HallInfoModel> Halls { get; set; } = new List<HallInfoModel>();
    }
}
