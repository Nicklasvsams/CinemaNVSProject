using System.ComponentModel.DataAnnotations;

namespace CinemasNVS.BLL.DTOs
{
    public class MovieActorRequest
    {
        [Required]
        public int MovieId { get; set; }
        [Required]
        public int ActorId { get; set; }
    }
}
