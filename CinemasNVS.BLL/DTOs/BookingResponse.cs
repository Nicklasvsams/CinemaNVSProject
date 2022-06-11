using System;
using System.Collections.Generic;

namespace CinemasNVS.BLL.DTOs
{
    public class BookingResponse
    {
        public int Id { get; set; }
        public DateTime BookingDate { get; set; }
        public int ShowingId { get; set; }
        public int CustomerId { get; set; }

        public BookingResponseCustomer CustomerResponse { get; set; }
        public BookingResponseShowing ShowingResponse { get; set; }
        public IEnumerable<BookingResponseSeating> SeatingResponses { get; set; }
    }

    public class BookingResponseSeating
    {
        public int Id { get; set; }
        public string Seat { get; set; }
    }

    public class BookingResponseShowing
    {
        public int Id { get; set; }
        public int Price { get; set; }
        public DateTime TimeOfShowing { get; set; }
        public int MovieId { get; set; }

        public ShowingResponseMovie MovieResponse { get; set; }
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
}
