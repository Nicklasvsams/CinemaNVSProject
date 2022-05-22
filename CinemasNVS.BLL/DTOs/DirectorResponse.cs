using System;
using System.Collections.Generic;

namespace CinemasNVS.BLL.DTOs
{
    public class DirectorResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImdbLink { get; set; }

        public IEnumerable<DirectorResponseMovie> Movies { get; set; }
    }

    public class DirectorResponseMovie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public float Rating { get; set; }
        public int RuntimeMinutes { get; set; }
        public DateTime ReleaseDate { get; set; }
        public bool IsRunning { get; set; }
        public string TrailerLink { get; set; }
        public string ImdbLink { get; set; }
        public int DirectorId { get; set; }
    }
}
