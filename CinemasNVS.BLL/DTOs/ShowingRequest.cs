using System;
using System.ComponentModel.DataAnnotations;

namespace CinemasNVS.BLL.DTOs
{
    public class ShowingRequest
    {
        [Required]
        public int Price { get; set; }
        [Required]
        public DateTime TimeOfShowing { get; set; }
        [Required]
        public int MovieId { get; set; }
    }
}
