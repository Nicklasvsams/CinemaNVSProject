using System;

namespace CinemasNVS.BLL.DTOs
{
    public class MovieResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public float Rating { get; set; }
        public int RuntimeMinutes { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int IsRunning { get; set; }
        public string TrailerLink { get; set; }
        public string ImdbLink { get; set; }
        public int DirectorId { get; set; }
    }
}
