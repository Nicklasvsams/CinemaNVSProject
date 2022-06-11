using CinemaNVS.DAL.Database.Entities.Movies;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaNVS.DAL.Database.Entities.Transactions
{
    public class Showing
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "int")]
        public int Price { get; set; }
        [Column(TypeName = "date")]
        public DateTime TimeOfShowing { get; set; }
        [Column(TypeName = "int")]
        public int MovieId { get; set; }

        public Movie Movie { get; set; }
        public ICollection<Booking> Bookings { get; set; }
    }
}
