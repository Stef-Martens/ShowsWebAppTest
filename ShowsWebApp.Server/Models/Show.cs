using System.ComponentModel.DataAnnotations;

namespace ShowsWebApp.Server.Models
{
    public class Show
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Language { get; set; }
        public string Description { get; set; }


        public ICollection<Season> Seasons { get; set; }
    }
}
