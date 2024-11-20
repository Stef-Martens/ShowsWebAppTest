namespace ShowsWebApp.Server.DTOs
{
    public class EpisodeDTO
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int SeasonId { get; set; }
    }
}
