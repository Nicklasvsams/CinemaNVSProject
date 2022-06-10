using System;
using System.ComponentModel.DataAnnotations;

namespace CinemasNVS.BLL.DTOs
{
    public class MovieRequest
    {
        [Required]
        [StringLength(100, ErrorMessage = "Title can not be longer than 100 characters long")]
        public string Title { get; set; }
        [Required]
        [Range(1, 1000,
        ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int RuntimeMinutes { get; set; }
        [Required]
        public DateTime ReleaseDate { get; set; }
        [Required]
        public bool IsRunning { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Title can not be longer than 100 characters long")]
        public string TrailerLink { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Title can not be longer than 100 characters long")]
        public string ImdbLink { get; set; }
        [Required]
        public int DirectorId { get; set; }
    }
}
