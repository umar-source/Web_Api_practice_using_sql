using System.ComponentModel.DataAnnotations;

namespace WebApiProject.Models
{
    public class Movie
    {
        public int id { get; set; }

        [Required]
        [StringLength(20)]
        public string? Title { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ReleaseDate { get; set; }

    }
}
