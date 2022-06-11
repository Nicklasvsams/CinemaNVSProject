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
        public int LoginId { get; set; }

        public CustomerResponseLogin LoginResponse { get; set; }
        public ICollection<CustomerResponseBooking> BookingResponses { get; set; }
    }

    public class CustomerResponseLogin
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; } = "Secret";
    }

    public class CustomerResponseBooking
    {
        public int Id { get; set; }
        public DateTime BookingDate { get; set; }
        public int CustomerId { get; set; }
        public int ShowingId { get; set; }
    }
}
