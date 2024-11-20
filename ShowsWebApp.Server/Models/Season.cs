using System.ComponentModel.DataAnnotations;

namespace ShowsWebApp.Server.Models
{
    public class Season
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int ShowId { get; set; }
        [Required]
        public int Number { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }


        public Show Show { get; set; }
        public ICollection<Episode> Episodes { get; set; }
    }
}
