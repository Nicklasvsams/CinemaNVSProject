using System.ComponentModel.DataAnnotations;

namespace CinemasNVS.BLL.DTOs
{
    public class DirectorRequest
    {
        [Required]
        [StringLength(100, ErrorMessage = "Name can not be longer than 100 characters long")]
        public string Name { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "IMDB link can not be longer than 100 characters long")]
        public string ImdbLink { get; set; }
    }
}
