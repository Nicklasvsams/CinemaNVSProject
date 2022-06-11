using CinemaNVS.DAL.Database.Entities.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaNVS.DAL.Database.Entities.Transactions
{
    public class Booking
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "date")]
        public DateTime BookingDate { get; set; }
        [Column(TypeName = "int")]
        public int CustomerId { get; set; }
        [Column(TypeName = "int")]
        public int ShowingId { get; set; }


        public Customer Customer { get; set; }
        public Showing Showing { get; set; }
        public ICollection<BookingSeating> BookingSeating { get; set; } = new List<BookingSeating>();
    }
}
