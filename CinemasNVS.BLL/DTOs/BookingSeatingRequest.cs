using System.ComponentModel.DataAnnotations;

namespace CinemasNVS.BLL.DTOs
{
    public class BookingSeatingRequest
    {
        [Required]
        public int bookingId { get; set; }
        [Required]
        public int seatingId { get; set; }
    }
}
