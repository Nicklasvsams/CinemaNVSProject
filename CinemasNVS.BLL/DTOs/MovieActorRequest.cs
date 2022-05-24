using System.ComponentModel.DataAnnotations;

namespace CinemasNVS.BLL.DTOs
{
    public class MovieActorRequest
    {
        [Required]
        public int movieId { get; set; }
        [Required]
        public int actorId { get; set; }
    }
}
