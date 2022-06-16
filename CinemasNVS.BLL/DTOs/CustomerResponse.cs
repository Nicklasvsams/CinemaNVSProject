using System;
using System.Collections.Generic;

namespace CinemasNVS.BLL.DTOs
{
    public class CustomerResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int PhoneNo { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }

        public ICollection<CustomerResponseBooking> BookingResponses { get; set; }
    }

    public class CustomerResponseBooking
    {
        public int Id { get; set; }
        public DateTime BookingDate { get; set; }
        public int CustomerId { get; set; }
        public int ShowingId { get; set; }

        public IEnumerable<BookingResponseSeating> SeatingResponses { get; set; }
        public BookingResponseShowing ShowingResponse { get; set; }
    }
}
