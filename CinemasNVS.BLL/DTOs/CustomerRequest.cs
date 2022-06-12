using System.ComponentModel.DataAnnotations;

namespace CinemasNVS.BLL.DTOs
{
    public class CustomerRequest
    {
        [Required]
        [StringLength(30, ErrorMessage = "First name can not be longer than 30 characters long")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(30, ErrorMessage = "Last name can not be longer than 30 characters long")]
        public string LastName { get; set; }
        [Required]
        [Range(10000000, 99999999,
        ErrorMessage = "Phone number must be 8 digits")]
        public int PhoneNo { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Email can not be longer than 100 characters long")]
        public string Email { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public int LoginId { get; set; }
    }
}
