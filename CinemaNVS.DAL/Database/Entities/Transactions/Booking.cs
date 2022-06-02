using CinemaNVS.DAL.Database.Entities.Movies;
using CinemaNVS.DAL.Database.Entities.Users;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaNVS.DAL.Database.Entities.Transactions
{
    public class Booking
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "int")]
        public int Price { get; set; }
        [Column(TypeName = "date")]
        public DateTime BookingDate { get; set; }
        [Column(TypeName = "int")]
        public int MovieId { get; set; }
        [Column(TypeName = "int")]
        public int CustomerId { get; set; }

        public Movie Movie { get; set; }
        public Customer Customer { get; set; }
    }
}
