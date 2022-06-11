using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaNVS.DAL.Database.Entities.Transactions
{
    public class Seating
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "nvarchar(3)")]
        public string Seat { get; set; }

        public ICollection<BookingSeating> BookingSeating { get; set; } = new List<BookingSeating>();
    }
}
