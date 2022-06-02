using System;

namespace CinemasNVS.BLL.DTOs
{
    public class BookingResponse
    {
        public int Id { get; set; }
        public int Price { get; set; }
        public DateTime BookingDate { get; set; }
        public int MovieId { get; set; }
        public int CustomerId { get; set; }

        public BookingResponseMovie MovieResponse { get; set; }
        public BookingResponseCustomer CustomerResponse { get; set; }
    }

    public class BookingResponseCustomer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int PhoneNo { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
    }

    public class BookingResponseMovie
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
