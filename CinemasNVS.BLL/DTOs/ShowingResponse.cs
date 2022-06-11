using System;
using System.Collections.Generic;

namespace CinemasNVS.BLL.DTOs
{
    public class ShowingResponse
    {
        public int Id { get; set; }
        public int Price { get; set; }
        public DateTime TimeOfShowing { get; set; }
        public int MovieId { get; set; }

        public ShowingResponseMovie MovieResponse { get; set; }
        public IEnumerable<ShowingResponseBooking> BookingResponses { get; set; }
    }

    public class ShowingResponseBooking
    {
        public int Id { get; set; }
        public DateTime BookingDate { get; set; }
        public int ShowingId { get; set; }
        public int CustomerId { get; set; }
    }

    public class ShowingResponseMovie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int RuntimeMinutes { get; set; }
        public DateTime ReleaseDate { get; set; }
        public bool IsRunning { get; set; }
        public string TrailerLink { get; set; }
        public string ImdbLink { get; set; }
        public int DirectorId { get; set; }
    }
}
