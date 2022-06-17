using CinemaNVS.DAL.Database.Entities.Movies;
using CinemaNVS.DAL.Database.Entities.Transactions;
using CinemaNVS.DAL.Database.Entities.Users;
using Microsoft.EntityFrameworkCore;
using System;

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
                    Name = "Angus MacLane",
                    ImdbLink = "https://www.imdb.com/name/nm0533691/"
                },
                new Director()
                {
                    Id = 2,
                    Name = "Colin Trevorrow",
                    ImdbLink = "https://www.imdb.com/name/nm1119880/"
                });

            modelBuilder.Entity<Actor>().HasData(
                new Actor()
                {
                    Id = 1,
                    Name = "Chris Evans",
                    ImdbLink = "https://www.imdb.com/name/nm0262635/"
                },
                new Actor()
                {
                    Id = 2,
                    Name = "Keke Palmer",
                    ImdbLink = "https://www.imdb.com/name/nm1551130/"
                },
                new Actor()
                {
                    Id = 3,
                    Name = "Peter Sohn",
                    ImdbLink = "https://www.imdb.com/name/nm0812307/"
                },
                new Actor()
                {
                    Id = 4,
                    Name = "Taiki Waititi",
                    ImdbLink = "https://www.imdb.com/name/nm0169806/"
                },
                new Actor()
                {
                    Id = 5,
                    Name = "Chris Pratt",
                    ImdbLink = "https://www.imdb.com/name/nm0695435/"
                },
                new Actor()
                {
                    Id = 6,
                    Name = "Bryce Dallas Howard",
                    ImdbLink = "https://www.imdb.com/name/nm0397171/"
                },
                new Actor()
                {
                    Id = 7,
                    Name = "Laura Dern",
                    ImdbLink = "https://www.imdb.com/name/nm0000368/"
                },
                new Actor()
                {
                    Id = 8,
                    Name = "Sam Neill",
                    ImdbLink = "https://www.imdb.com/name/nm0000554/"
                });

            modelBuilder.Entity<Movie>().HasData(
                new Movie()
                {
                    Id = 1,
                    Title = "Lightyear",
                    DirectorId = 1,
                    ReleaseDate = new System.DateTime(2022, 06, 16),
                    IsRunning = 1,
                    RuntimeMinutes = 100,
                    TrailerLink = "https://www.imdb.com/video/vi1034797593/?playlistId=tt10298810",
                    ImdbLink = "https://www.imdb.com/title/tt10298810/"
                },
                new Movie()
                {
                    Id = 2,
                    Title = "Jurassic World: Dominion",
                    DirectorId = 2,
                    ReleaseDate = new System.DateTime(2022, 06, 09),
                    IsRunning = 1,
                    RuntimeMinutes = 146,
                    TrailerLink = "https://www.imdb.com/video/vi764854809/?playlistId=tt8041270",
                    ImdbLink = "https://www.imdb.com/title/tt8041270/"
                });

            modelBuilder.Entity<MovieActor>().HasData(
                new MovieActor()
                {
                    Id = 1,
                    MovieId = 1,
                    ActorId = 1
                },
                new MovieActor()
                {
                    Id = 2,
                    MovieId = 1,
                    ActorId = 2
                },
                new MovieActor()
                {
                    Id = 3,
                    MovieId = 1,
                    ActorId = 3
                },
                new MovieActor()
                {
                    Id = 4,
                    MovieId = 1,
                    ActorId = 4
                },
                new MovieActor()
                {
                    Id = 5,
                    MovieId = 2,
                    ActorId = 5
                },
                new MovieActor()
                {
                    Id = 6,
                    MovieId = 2,
                    ActorId = 6
                }, 
                new MovieActor()
                {
                    Id = 5,
                    MovieId = 2,
                    ActorId = 7
                }, new MovieActor()
                {
                    Id = 5,
                    MovieId = 2,
                    ActorId = 8
                });


            modelBuilder.Entity<Login>().HasData(
                new Login()
                {
                    Id = 1,
                    Username = "Bobby",
                    Password = "Passw0rd",
                    IsAdmin = "no",
                    CustomerId = 2
                },
                new Login(){
                    Id = 2,
                    Username = "admin",
                    Password = "admin",
                    IsAdmin = "yes",
                    CustomerId = 1
                });

            modelBuilder.Entity<Customer>().HasData(
                new Customer()
                {
                    Id = 1,
                    FirstName = "admin",
                    LastName = "admin",
                    PhoneNo = 51515151,
                    Email = "Test@gmail.com",
                    IsActive = "yes"
                },
                new Customer()
                {
                    Id = 2,
                    FirstName = "Bob",
                    LastName = "Levinsen",
                    PhoneNo = 11223344,
                    Email = "Test@gmail.com",
                    IsActive = "yes"
                });

            modelBuilder.Entity<Showing>().HasData(
                new Showing()
                {
                    Id = 1,
                    MovieId = 1,
                    Price = 140,
                    TimeOfShowing = new DateTime(2022, 06, 28, 13, 30, 00)
                },
                new Showing()
                {
                    Id = 2,
                    MovieId = 1,
                    Price = 140,
                    TimeOfShowing = new DateTime(2022, 06, 28, 15, 30, 00)
                },
                new Showing()
                {
                    Id = 3,
                    MovieId = 1,
                    Price = 140,
                    TimeOfShowing = new DateTime(2022, 06, 28, 17, 30, 00)
                },
                new Showing()
                {
                    Id = 4,
                    MovieId = 1,
                    Price = 140,
                    TimeOfShowing = new DateTime(2022, 06, 28, 19, 30, 00)
                }, 
                new Showing()
                {
                    Id = 5,
                    MovieId = 2,
                    Price = 160,
                    TimeOfShowing = new DateTime(2022, 06, 27, 13, 30, 00)
                },
                new Showing()
                {
                    Id = 6,
                    MovieId = 2,
                    Price = 160,
                    TimeOfShowing = new DateTime(2022, 06, 27, 15, 30, 00)
                },
                new Showing()
                {
                    Id = 7,
                    MovieId = 2,
                    Price = 160,
                    TimeOfShowing = new DateTime(2022, 06, 27, 17, 30, 00)
                },
                new Showing()
                {
                    Id = 8,
                    MovieId = 2,
                    Price = 160,
                    TimeOfShowing = new DateTime(2022, 06, 27, 19, 30, 00)
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
