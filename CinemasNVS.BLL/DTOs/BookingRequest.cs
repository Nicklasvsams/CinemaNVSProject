using System;
using System.ComponentModel.DataAnnotations;

namespace CinemasNVS.BLL.DTOs
{
    public class BookingRequest
    {
        public DateTime BookingDate { get; set; } = DateTime.Now;
        [Required]
        public int ShowingId { get; set; }
        [Required]
        public int CustomerId { get; set; }
        public string Seat { get; set; } = "";
    }
}
