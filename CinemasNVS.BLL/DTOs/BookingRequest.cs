using System;
using System.ComponentModel.DataAnnotations;

namespace CinemasNVS.BLL.DTOs
{
    public class BookingRequest
    {
        [Required]
        public DateTime BookingDate { get; set; }
        [Required]
        public int ShowingId { get; set; }
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public string Seat { get; set; }
    }
}
