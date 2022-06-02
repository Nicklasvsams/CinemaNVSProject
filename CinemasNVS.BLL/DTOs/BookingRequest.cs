using System;
using System.ComponentModel.DataAnnotations;

namespace CinemasNVS.BLL.DTOs
{
    public class BookingRequest
    {
        [Required]
        [Range(1, 10000,
        ErrorMessage = "Price must be between 1 and 10000")]
        public int Price { get; set; }
        [Required]
        public DateTime BookingDate { get; set; }
        [Required]
        public int MovieId { get; set; }
        [Required]
        public int CustomerId { get; set; }
    }
}
