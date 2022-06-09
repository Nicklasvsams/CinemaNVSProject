using CinemaNVS.DAL.Database.Entities.Movies;
using CinemaNVS.DAL.Database.Entities.Transactions;
using CinemaNVS.DAL.Database.Entities.Users;
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
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Login> Logins { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MovieActor>()
                .HasOne(m => m.Movie)
                .WithMany(ma => ma.MovieActor)
                .HasForeignKey(mi => mi.MovieId);

            modelBuilder.Entity<MovieActor>()
                .HasOne(a => a.Actor)
                .WithMany(ma => ma.MovieActor)
                .HasForeignKey(ai => ai.ActorId);

            modelBuilder.Entity<Login>()
                .HasIndex(l => l.Username)
                .IsUnique();

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
                    Id = 1,
                    MovieId = 1,
                    ActorId = 1
                });

            modelBuilder.Entity<Customer>().HasData(
                new Customer()
                {
                    Id = 1,
                    FirstName = "Bob",
                    LastName = "Levinsen",
                    PhoneNo = 11223344,
                    Email = "Test@gmail.com",
                    IsActive = "yes"
                });

            modelBuilder.Entity<Login>().HasData(
                new Login()
                {
                    Id = 1,
                    Username = "Bobby",
                    Password = "Passw0rd",
                    IsAdmin = "no",
                    CustomerId = 1
                });

            modelBuilder.Entity<Login>().HasData(
                new Login()
                {
                    Id = 2,
                    Username = "admin",
                    Password = "admin",
                    IsAdmin = "yes"
                });

            modelBuilder.Entity<Booking>().HasData(
                new Booking()
                {
                    Id = 1,
                    BookingDate = new System.DateTime(2022, 05, 24),
                    Price = 140,
                    CustomerId = 1,
                    MovieId = 1
                });
        }
    }
}
