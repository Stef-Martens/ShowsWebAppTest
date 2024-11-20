using System.ComponentModel.DataAnnotations;

namespace ShowsWebApp.Server.Models
{
    public class Episode
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int Number { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }


        public int SeasonId { get; set; }
        public Season Season { get; set; }
    }
}
