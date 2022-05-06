using System.Collections.Generic;

namespace CinemasNVS.BLL.DTOs
{
    public class ActorResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImdbLink { get; set; }

        public IEnumerable<MovieResponse> Movies { get; set; }
    }
}
