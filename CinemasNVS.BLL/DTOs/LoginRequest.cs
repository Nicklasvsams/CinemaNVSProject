using System.ComponentModel.DataAnnotations;

namespace CinemasNVS.BLL.DTOs
{
    public class LoginRequest
    {
        [Required]
        [StringLength(450, ErrorMessage = "Username can not be longer than 450 characters long")]
        public string Username { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Username can not be longer than 50 characters long")]
        public string Password { get; set; }
        [Required]
        public bool IsAdmin { get; set; }
    }
}
