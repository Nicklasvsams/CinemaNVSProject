using System.Collections.Generic;

namespace CinemasNVS.BLL.DTOs
{
    public class LoginResponse
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; } = "Secret";
        public bool IsAdmin { get; set; }
        public bool IsAuthorized { get; set; }
        public int CustomerId { get; set; }

        public LoginResponseCustomer CustomerResponse { get; set; }
    }

    public class LoginResponseCustomer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int PhoneNo { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }

        public ICollection<CustomerResponseBooking> bookingResponses { get; set; }
    }
}
