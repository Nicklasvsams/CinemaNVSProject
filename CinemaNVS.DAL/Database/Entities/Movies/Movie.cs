using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaNVS.DAL.Database.Entities.Movies
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string Title { get; set; }
        [Column(TypeName = "decimal(2,1)")]
        public float Rating { get; set; }
        [Column(TypeName = "int")]
        public int RuntimeMinutes { get; set; }
        [Column(TypeName = "date")]
        public DateTime ReleaseDate { get; set; }
        [Column(TypeName = "int")]
        public int IsRunning { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string TrailerLink { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string ImdbLink { get; set; }
        [Column(TypeName = "int")]
        public int DirectorId { get; set; }

        public Director Director { get; set; }
        public IEnumerable<Actor> Actors { get; set; } = new List<Actor>();
    }
}
