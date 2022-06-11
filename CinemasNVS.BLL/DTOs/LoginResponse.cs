namespace CinemasNVS.BLL.DTOs
{
    public class LoginResponse
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; } = "Secret";
        public bool IsAdmin { get; set; }
        public bool IsAuthorized { get; set; }
    }
}
