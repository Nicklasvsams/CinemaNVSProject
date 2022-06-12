using CinemaNVS.DAL.Database.Entities.Movies;
using CinemaNVS.DAL.Database.Entities.Transactions;
using CinemaNVS.DAL.Database.Entities.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;

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
        public DbSet<Showing> Showings { get; set; }
        public DbSet<Seating> Seatings { get; set; }
        public DbSet<BookingSeating> BookingSeating { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MovieActor>()
                .HasKey(ma => new {ma.MovieId, ma.ActorId});

            modelBuilder.Entity<MovieActor>()
                .Property<int>("Id")
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<MovieActor>()
                .HasOne(m => m.Movie)
                .WithMany(ma => ma.MovieActor)
                .HasForeignKey(mi => mi.MovieId);

            modelBuilder.Entity<MovieActor>()
                .HasOne(a => a.Actor)
                .WithMany(ma => ma.MovieActor)
                .HasForeignKey(ai => ai.ActorId);

            modelBuilder.Entity<BookingSeating>()
                .HasKey(bs => new { bs.BookingId, bs.SeatingId });

            modelBuilder.Entity<BookingSeating>()
                .Property<int>("Id")
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<BookingSeating>()
                .HasOne(b => b.Booking)
                .WithMany(bs => bs.BookingSeating)
                .HasForeignKey(bi => bi.BookingId);

            modelBuilder.Entity<BookingSeating>()
                .HasOne(s => s.Seating)
                .WithMany(bs => bs.BookingSeating)
                .HasForeignKey(si => si.SeatingId);

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


            modelBuilder.Entity<Login>().HasData(
                new Login()
                {
                    Id = 1,
                    Username = "Bobby",
                    Password = "Passw0rd",
                    IsAdmin = "no"
                });

            modelBuilder.Entity<Customer>().HasData(
                new Customer()
                {
                    Id = 1,
                    FirstName = "Bob",
                    LastName = "Levinsen",
                    PhoneNo = 11223344,
                    Email = "Test@gmail.com",
                    IsActive = "yes",
                    LoginId = 1
                });

            modelBuilder.Entity<Login>().HasData(
                new Login()
                {
                    Id = 2,
                    Username = "admin",
                    Password = "admin",
                    IsAdmin = "yes"
                });

            modelBuilder.Entity<Showing>().HasData(
                new Showing()
                {
                    Id = 1,
                    MovieId = 1,
                    Price = 140,
                    TimeOfShowing = new DateTime(2022, 06, 28, 19, 30, 00)
                });

            modelBuilder.Entity<Booking>().HasData(
                new Booking()
                {
                    Id = 1,
                    BookingDate = new System.DateTime(2022, 05, 24),
                    CustomerId = 1,
                    ShowingId = 1
                });

            char[] charList = "ABCDEFGHIJ".ToCharArray();
            int seatingId = 1;

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    modelBuilder.Entity<Seating>().HasData(
                        new Seating()
                        {
                            Id = seatingId,
                            Seat = charList[i].ToString() + (j+1).ToString()
                        });

                    seatingId++;
                }
            }

            modelBuilder.Entity<BookingSeating>().HasData(
                new BookingSeating()
                {
                    Id = 1,
                    BookingId = 1,
                    SeatingId = 1
                });
        }
    }
}
