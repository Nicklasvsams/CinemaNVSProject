using CinemaNVS.DAL.Database.Entities.Transactions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaNVS.DAL.Database.Entities.Users
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "nvarchar(30)")]
        public string FirstName { get; set; }
        [Column(TypeName = "nvarchar(30)")]
        public string LastName { get; set; }
        [Column(TypeName = "int")]
        public int PhoneNo { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string Email { get; set; }
        [Column(TypeName = "nvarchar(3)")]
        public string IsActive { get; set; }

        public ICollection<Booking> Bookings { get; set; }
    }
}
