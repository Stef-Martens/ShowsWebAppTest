namespace ShowsWebApp.Server.DTOs
{
    public class ShowDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Language { get; set; }
        public string Description { get; set; }
        public List<SeasonSummaryDTO>? Seasons { get; set; }
    }
}
