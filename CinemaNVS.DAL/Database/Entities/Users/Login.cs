using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaNVS.DAL.Database.Entities.Users
{
    public class Login
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "nvarchar(450)")]
        public string Username { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string Password { get; set; }
        [Column(TypeName = "nvarchar(3)")]
        public string IsAdmin { get; set; }
    }
}
