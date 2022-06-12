using System;
using System.Collections.Generic;

namespace CinemasNVS.BLL.DTOs
{
    public class MovieResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int RuntimeMinutes { get; set; }
        public DateTime ReleaseDate { get; set; }
        public bool IsRunning { get; set; }
        public string TrailerLink { get; set; }
        public string ImdbLink { get; set; }
        public int DirectorId { get; set; }

        public IEnumerable<MovieResponseShowing> ShowingResponses { get; set; }
        public IEnumerable<MovieResponseActor> ActorResponses { get; set; }
        public MovieResponseDirector DirectorResponse { get; set; }
    }

    public class MovieResponseShowing
    {
        public int Id { get; set; }
        public int Price { get; set; }
        public DateTime TimeOfShowing { get; set; }
        public int MovieId { get; set; }
    }

    public class MovieResponseDirector
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImdbLink { get; set; }
    }

    public class MovieResponseActor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImdbLink { get; set; }
    }
}
