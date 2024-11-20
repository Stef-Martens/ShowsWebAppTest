namespace ShowsWebApp.Server.DTOs
{
    public class SeasonDTO
    {
        public int Id { get; set; }
        public int ShowId { get; set; }
        public int Number { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<EpisodeDTO>? Episodes { get; set; }
    }
}
