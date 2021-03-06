using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaNVS.DAL.Database.Entities.Movies
{
    public class Actor
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string ImdbLink { get; set; }

        public ICollection<MovieActor> MovieActor { get; set; } = new List<MovieActor>();
    }
}
