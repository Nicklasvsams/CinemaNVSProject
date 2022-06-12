using System.ComponentModel.DataAnnotations;

namespace CinemasNVS.BLL.DTOs
{
    public class BookingSeatingRequest
    {
        [Required]
        public int BookingId { get; set; }
        [Required]
        public int SeatingId { get; set; }
    }
}
