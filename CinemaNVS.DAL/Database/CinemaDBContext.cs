using CinemaNVS.DAL.Database.Entities.Movies;
using Microsoft.EntityFrameworkCore;

namespace CinemaNVS.DAL.Database
{
    public class CinemaDBContext : DbContext
    {
        public CinemaDBContext() { }

        public CinemaDBContext(DbContextOptions<CinemaDBContext> options) : base(options) { }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<MovieActor> MovieActor { get; set; }
        public DbSet<Director> Directors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MovieActor>()
                .HasKey(ma => new {ma.MovieId, ma.ActorId});

            modelBuilder.Entity<MovieActor>()
                .HasOne(m => m.Movie)
                .WithMany(ma => ma.MovieActor)
                .HasForeignKey(mi => mi.MovieId);

            modelBuilder.Entity<MovieActor>()
                .HasOne(a => a.Actor)
                .WithMany(ma => ma.MovieActor)
                .HasForeignKey(ai => ai.ActorId);

            modelBuilder.Entity<Director>().HasData(
                new Director()
                {
                    Id = 1,
                    Name = "Quentin Tarantino",
                    ImdbLink = "https://www.imdb.com/name/nm0000233/"
                });

            modelBuilder.Entity<Actor>().HasData(
                new Actor()
                {
                    Id = 1,
                    Name = "Jamie Foxx",
                    ImdbLink = "https://www.imdb.com/name/nm0004937/"
                });

            modelBuilder.Entity<Movie>().HasData(
                new Movie()
                {
                    Id = 1,
                    Title = "Django Unchained",
                    DirectorId = 1,
                    ReleaseDate = new System.DateTime(2013, 01, 24),
                    IsRunning = 0,
                    RuntimeMinutes = 165,
                    Rating = 8.4F,
                    TrailerLink = "https://www.youtube.com/watch?v=0fUCuvNlOCg",
                    ImdbLink = "https://www.imdb.com/title/tt1853728/"
                });

            modelBuilder.Entity<MovieActor>().HasData(
                new MovieActor()
                {
                    MovieId = 1,
                    ActorId = 1
                });
        }
    }
}
