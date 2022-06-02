namespace CinemasNVS.BLL.DTOs
{
    public class LoginResponse
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public int? CustomerId { get; set; }

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
    }
}
