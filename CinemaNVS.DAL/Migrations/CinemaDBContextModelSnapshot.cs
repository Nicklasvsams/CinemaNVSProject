// <auto-generated />
using System;
using CinemaNVS.DAL.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CinemaNVS.DAL.Migrations
{
    [DbContext(typeof(CinemaDBContext))]
    partial class CinemaDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.16")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CinemaNVS.DAL.Database.Entities.Movies.Actor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ImdbLink")
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Actors");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ImdbLink = "https://www.imdb.com/name/nm0262635/",
                            Name = "Chris Evans"
                        },
                        new
                        {
                            Id = 2,
                            ImdbLink = "https://www.imdb.com/name/nm1551130/",
                            Name = "Keke Palmer"
                        },
                        new
                        {
                            Id = 3,
                            ImdbLink = "https://www.imdb.com/name/nm0812307/",
                            Name = "Peter Sohn"
                        },
                        new
                        {
                            Id = 4,
                            ImdbLink = "https://www.imdb.com/name/nm0169806/",
                            Name = "Taiki Waititi"
                        },
                        new
                        {
                            Id = 5,
                            ImdbLink = "https://www.imdb.com/name/nm0695435/",
                            Name = "Chris Pratt"
                        },
                        new
                        {
                            Id = 6,
                            ImdbLink = "https://www.imdb.com/name/nm0397171/",
                            Name = "Bryce Dallas Howard"
                        },
                        new
                        {
                            Id = 7,
                            ImdbLink = "https://www.imdb.com/name/nm0000368/",
                            Name = "Laura Dern"
                        },
                        new
                        {
                            Id = 8,
                            ImdbLink = "https://www.imdb.com/name/nm0000554/",
                            Name = "Sam Neill"
                        });
                });

            modelBuilder.Entity("CinemaNVS.DAL.Database.Entities.Movies.Director", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ImdbLink")
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Directors");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ImdbLink = "https://www.imdb.com/name/nm0533691/",
                            Name = "Angus MacLane"
                        },
                        new
                        {
                            Id = 2,
                            ImdbLink = "https://www.imdb.com/name/nm1119880/",
                            Name = "Colin Trevorrow"
                        });
                });

            modelBuilder.Entity("CinemaNVS.DAL.Database.Entities.Movies.Movie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DirectorId")
                        .HasColumnType("int");

                    b.Property<string>("ImdbLink")
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("IsRunning")
                        .HasColumnType("int");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("date");

                    b.Property<int>("RuntimeMinutes")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("TrailerLink")
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("DirectorId");

                    b.ToTable("Movies");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DirectorId = 1,
                            ImdbLink = "https://www.imdb.com/title/tt10298810/",
                            IsRunning = 1,
                            ReleaseDate = new DateTime(2022, 6, 16, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            RuntimeMinutes = 100,
                            Title = "Lightyear",
                            TrailerLink = "https://www.imdb.com/video/vi1034797593/?playlistId=tt10298810"
                        },
                        new
                        {
                            Id = 2,
                            DirectorId = 2,
                            ImdbLink = "https://www.imdb.com/title/tt8041270/",
                            IsRunning = 1,
                            ReleaseDate = new DateTime(2022, 6, 9, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            RuntimeMinutes = 146,
                            Title = "Jurassic World: Dominion",
                            TrailerLink = "https://www.imdb.com/video/vi764854809/?playlistId=tt8041270"
                        });
                });

            modelBuilder.Entity("CinemaNVS.DAL.Database.Entities.Movies.MovieActor", b =>
                {
                    b.Property<int>("MovieId")
                        .HasColumnType("int");

                    b.Property<int>("ActorId")
                        .HasColumnType("int");

                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.HasKey("MovieId", "ActorId");

                    b.HasIndex("ActorId");

                    b.ToTable("MovieActor");

                    b.HasData(
                        new
                        {
                            MovieId = 1,
                            ActorId = 1,
                            Id = 1
                        },
                        new
                        {
                            MovieId = 1,
                            ActorId = 2,
                            Id = 2
                        },
                        new
                        {
                            MovieId = 1,
                            ActorId = 3,
                            Id = 3
                        },
                        new
                        {
                            MovieId = 1,
                            ActorId = 4,
                            Id = 4
                        },
                        new
                        {
                            MovieId = 2,
                            ActorId = 5,
                            Id = 5
                        },
                        new
                        {
                            MovieId = 2,
                            ActorId = 6,
                            Id = 6
                        },
                        new
                        {
                            MovieId = 2,
                            ActorId = 7,
                            Id = 5
                        },
                        new
                        {
                            MovieId = 2,
                            ActorId = 8,
                            Id = 5
                        });
                });

            modelBuilder.Entity("CinemaNVS.DAL.Database.Entities.Transactions.Booking", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("BookingDate")
                        .HasColumnType("date");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<int>("ShowingId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("ShowingId");

                    b.ToTable("Bookings");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BookingDate = new DateTime(2022, 5, 24, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CustomerId = 1,
                            ShowingId = 1
                        });
                });

            modelBuilder.Entity("CinemaNVS.DAL.Database.Entities.Transactions.BookingSeating", b =>
                {
                    b.Property<int>("BookingId")
                        .HasColumnType("int");

                    b.Property<int>("SeatingId")
                        .HasColumnType("int");

                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.HasKey("BookingId", "SeatingId");

                    b.HasIndex("SeatingId");

                    b.ToTable("BookingSeating");

                    b.HasData(
                        new
                        {
                            BookingId = 1,
                            SeatingId = 1,
                            Id = 1
                        });
                });

            modelBuilder.Entity("CinemaNVS.DAL.Database.Entities.Transactions.Seating", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Seat")
                        .HasColumnType("nvarchar(3)");

                    b.HasKey("Id");

                    b.ToTable("Seatings");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Seat = "A1"
                        },
                        new
                        {
                            Id = 2,
                            Seat = "A2"
                        },
                        new
                        {
                            Id = 3,
                            Seat = "A3"
                        },
                        new
                        {
                            Id = 4,
                            Seat = "A4"
                        },
                        new
                        {
                            Id = 5,
                            Seat = "A5"
                        },
                        new
                        {
                            Id = 6,
                            Seat = "A6"
                        },
                        new
                        {
                            Id = 7,
                            Seat = "A7"
                        },
                        new
                        {
                            Id = 8,
                            Seat = "A8"
                        },
                        new
                        {
                            Id = 9,
                            Seat = "A9"
                        },
                        new
                        {
                            Id = 10,
                            Seat = "A10"
                        },
                        new
                        {
                            Id = 11,
                            Seat = "B1"
                        },
                        new
                        {
                            Id = 12,
                            Seat = "B2"
                        },
                        new
                        {
                            Id = 13,
                            Seat = "B3"
                        },
                        new
                        {
                            Id = 14,
                            Seat = "B4"
                        },
                        new
                        {
                            Id = 15,
                            Seat = "B5"
                        },
                        new
                        {
                            Id = 16,
                            Seat = "B6"
                        },
                        new
                        {
                            Id = 17,
                            Seat = "B7"
                        },
                        new
                        {
                            Id = 18,
                            Seat = "B8"
                        },
                        new
                        {
                            Id = 19,
                            Seat = "B9"
                        },
                        new
                        {
                            Id = 20,
                            Seat = "B10"
                        },
                        new
                        {
                            Id = 21,
                            Seat = "C1"
                        },
                        new
                        {
                            Id = 22,
                            Seat = "C2"
                        },
                        new
                        {
                            Id = 23,
                            Seat = "C3"
                        },
                        new
                        {
                            Id = 24,
                            Seat = "C4"
                        },
                        new
                        {
                            Id = 25,
                            Seat = "C5"
                        },
                        new
                        {
                            Id = 26,
                            Seat = "C6"
                        },
                        new
                        {
                            Id = 27,
                            Seat = "C7"
                        },
                        new
                        {
                            Id = 28,
                            Seat = "C8"
                        },
                        new
                        {
                            Id = 29,
                            Seat = "C9"
                        },
                        new
                        {
                            Id = 30,
                            Seat = "C10"
                        },
                        new
                        {
                            Id = 31,
                            Seat = "D1"
                        },
                        new
                        {
                            Id = 32,
                            Seat = "D2"
                        },
                        new
                        {
                            Id = 33,
                            Seat = "D3"
                        },
                        new
                        {
                            Id = 34,
                            Seat = "D4"
                        },
                        new
                        {
                            Id = 35,
                            Seat = "D5"
                        },
                        new
                        {
                            Id = 36,
                            Seat = "D6"
                        },
                        new
                        {
                            Id = 37,
                            Seat = "D7"
                        },
                        new
                        {
                            Id = 38,
                            Seat = "D8"
                        },
                        new
                        {
                            Id = 39,
                            Seat = "D9"
                        },
                        new
                        {
                            Id = 40,
                            Seat = "D10"
                        },
                        new
                        {
                            Id = 41,
                            Seat = "E1"
                        },
                        new
                        {
                            Id = 42,
                            Seat = "E2"
                        },
                        new
                        {
                            Id = 43,
                            Seat = "E3"
                        },
                        new
                        {
                            Id = 44,
                            Seat = "E4"
                        },
                        new
                        {
                            Id = 45,
                            Seat = "E5"
                        },
                        new
                        {
                            Id = 46,
                            Seat = "E6"
                        },
                        new
                        {
                            Id = 47,
                            Seat = "E7"
                        },
                        new
                        {
                            Id = 48,
                            Seat = "E8"
                        },
                        new
                        {
                            Id = 49,
                            Seat = "E9"
                        },
                        new
                        {
                            Id = 50,
                            Seat = "E10"
                        },
                        new
                        {
                            Id = 51,
                            Seat = "F1"
                        },
                        new
                        {
                            Id = 52,
                            Seat = "F2"
                        },
                        new
                        {
                            Id = 53,
                            Seat = "F3"
                        },
                        new
                        {
                            Id = 54,
                            Seat = "F4"
                        },
                        new
                        {
                            Id = 55,
                            Seat = "F5"
                        },
                        new
                        {
                            Id = 56,
                            Seat = "F6"
                        },
                        new
                        {
                            Id = 57,
                            Seat = "F7"
                        },
                        new
                        {
                            Id = 58,
                            Seat = "F8"
                        },
                        new
                        {
                            Id = 59,
                            Seat = "F9"
                        },
                        new
                        {
                            Id = 60,
                            Seat = "F10"
                        },
                        new
                        {
                            Id = 61,
                            Seat = "G1"
                        },
                        new
                        {
                            Id = 62,
                            Seat = "G2"
                        },
                        new
                        {
                            Id = 63,
                            Seat = "G3"
                        },
                        new
                        {
                            Id = 64,
                            Seat = "G4"
                        },
                        new
                        {
                            Id = 65,
                            Seat = "G5"
                        },
                        new
                        {
                            Id = 66,
                            Seat = "G6"
                        },
                        new
                        {
                            Id = 67,
                            Seat = "G7"
                        },
                        new
                        {
                            Id = 68,
                            Seat = "G8"
                        },
                        new
                        {
                            Id = 69,
                            Seat = "G9"
                        },
                        new
                        {
                            Id = 70,
                            Seat = "G10"
                        },
                        new
                        {
                            Id = 71,
                            Seat = "H1"
                        },
                        new
                        {
                            Id = 72,
                            Seat = "H2"
                        },
                        new
                        {
                            Id = 73,
                            Seat = "H3"
                        },
                        new
                        {
                            Id = 74,
                            Seat = "H4"
                        },
                        new
                        {
                            Id = 75,
                            Seat = "H5"
                        },
                        new
                        {
                            Id = 76,
                            Seat = "H6"
                        },
                        new
                        {
                            Id = 77,
                            Seat = "H7"
                        },
                        new
                        {
                            Id = 78,
                            Seat = "H8"
                        },
                        new
                        {
                            Id = 79,
                            Seat = "H9"
                        },
                        new
                        {
                            Id = 80,
                            Seat = "H10"
                        },
                        new
                        {
                            Id = 81,
                            Seat = "I1"
                        },
                        new
                        {
                            Id = 82,
                            Seat = "I2"
                        },
                        new
                        {
                            Id = 83,
                            Seat = "I3"
                        },
                        new
                        {
                            Id = 84,
                            Seat = "I4"
                        },
                        new
                        {
                            Id = 85,
                            Seat = "I5"
                        },
                        new
                        {
                            Id = 86,
                            Seat = "I6"
                        },
                        new
                        {
                            Id = 87,
                            Seat = "I7"
                        },
                        new
                        {
                            Id = 88,
                            Seat = "I8"
                        },
                        new
                        {
                            Id = 89,
                            Seat = "I9"
                        },
                        new
                        {
                            Id = 90,
                            Seat = "I10"
                        },
                        new
                        {
                            Id = 91,
                            Seat = "J1"
                        },
                        new
                        {
                            Id = 92,
                            Seat = "J2"
                        },
                        new
                        {
                            Id = 93,
                            Seat = "J3"
                        },
                        new
                        {
                            Id = 94,
                            Seat = "J4"
                        },
                        new
                        {
                            Id = 95,
                            Seat = "J5"
                        },
                        new
                        {
                            Id = 96,
                            Seat = "J6"
                        },
                        new
                        {
                            Id = 97,
                            Seat = "J7"
                        },
                        new
                        {
                            Id = 98,
                            Seat = "J8"
                        },
                        new
                        {
                            Id = 99,
                            Seat = "J9"
                        },
                        new
                        {
                            Id = 100,
                            Seat = "J10"
                        });
                });

            modelBuilder.Entity("CinemaNVS.DAL.Database.Entities.Transactions.Showing", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("MovieId")
                        .HasColumnType("int");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<DateTime>("TimeOfShowing")
                        .HasColumnType("smalldatetime");

                    b.HasKey("Id");

                    b.HasIndex("MovieId");

                    b.ToTable("Showings");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            MovieId = 1,
                            Price = 140,
                            TimeOfShowing = new DateTime(2022, 6, 28, 13, 30, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 2,
                            MovieId = 1,
                            Price = 140,
                            TimeOfShowing = new DateTime(2022, 6, 28, 15, 30, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 3,
                            MovieId = 1,
                            Price = 140,
                            TimeOfShowing = new DateTime(2022, 6, 28, 17, 30, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 4,
                            MovieId = 1,
                            Price = 140,
                            TimeOfShowing = new DateTime(2022, 6, 28, 19, 30, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 5,
                            MovieId = 2,
                            Price = 160,
                            TimeOfShowing = new DateTime(2022, 6, 27, 13, 30, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 6,
                            MovieId = 2,
                            Price = 160,
                            TimeOfShowing = new DateTime(2022, 6, 27, 15, 30, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 7,
                            MovieId = 2,
                            Price = 160,
                            TimeOfShowing = new DateTime(2022, 6, 27, 17, 30, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 8,
                            MovieId = 2,
                            Price = 160,
                            TimeOfShowing = new DateTime(2022, 6, 27, 19, 30, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("CinemaNVS.DAL.Database.Entities.Users.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("IsActive")
                        .HasColumnType("nvarchar(3)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(30)");

                    b.Property<int>("PhoneNo")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Customers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "Test@gmail.com",
                            FirstName = "admin",
                            IsActive = "yes",
                            LastName = "admin",
                            PhoneNo = 51515151
                        },
                        new
                        {
                            Id = 2,
                            Email = "Test@gmail.com",
                            FirstName = "Bob",
                            IsActive = "yes",
                            LastName = "Levinsen",
                            PhoneNo = 11223344
                        });
                });

            modelBuilder.Entity("CinemaNVS.DAL.Database.Entities.Users.Login", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<string>("IsAdmin")
                        .HasColumnType("nvarchar(3)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("Username")
                        .IsUnique()
                        .HasFilter("[Username] IS NOT NULL");

                    b.ToTable("Logins");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CustomerId = 2,
                            IsAdmin = "no",
                            Password = "Passw0rd",
                            Username = "Bobby"
                        },
                        new
                        {
                            Id = 2,
                            CustomerId = 1,
                            IsAdmin = "yes",
                            Password = "admin",
                            Username = "admin"
                        });
                });

            modelBuilder.Entity("CinemaNVS.DAL.Database.Entities.Movies.Movie", b =>
                {
                    b.HasOne("CinemaNVS.DAL.Database.Entities.Movies.Director", "Director")
                        .WithMany("Movies")
                        .HasForeignKey("DirectorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Director");
                });

            modelBuilder.Entity("CinemaNVS.DAL.Database.Entities.Movies.MovieActor", b =>
                {
                    b.HasOne("CinemaNVS.DAL.Database.Entities.Movies.Actor", "Actor")
                        .WithMany("MovieActor")
                        .HasForeignKey("ActorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CinemaNVS.DAL.Database.Entities.Movies.Movie", "Movie")
                        .WithMany("MovieActor")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Actor");

                    b.Navigation("Movie");
                });

            modelBuilder.Entity("CinemaNVS.DAL.Database.Entities.Transactions.Booking", b =>
                {
                    b.HasOne("CinemaNVS.DAL.Database.Entities.Users.Customer", "Customer")
                        .WithMany("Bookings")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CinemaNVS.DAL.Database.Entities.Transactions.Showing", "Showing")
                        .WithMany("Bookings")
                        .HasForeignKey("ShowingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Showing");
                });

            modelBuilder.Entity("CinemaNVS.DAL.Database.Entities.Transactions.BookingSeating", b =>
                {
                    b.HasOne("CinemaNVS.DAL.Database.Entities.Transactions.Booking", "Booking")
                        .WithMany("BookingSeating")
                        .HasForeignKey("BookingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CinemaNVS.DAL.Database.Entities.Transactions.Seating", "Seating")
                        .WithMany("BookingSeating")
                        .HasForeignKey("SeatingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Booking");

                    b.Navigation("Seating");
                });

            modelBuilder.Entity("CinemaNVS.DAL.Database.Entities.Transactions.Showing", b =>
                {
                    b.HasOne("CinemaNVS.DAL.Database.Entities.Movies.Movie", "Movie")
                        .WithMany("Showings")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Movie");
                });

            modelBuilder.Entity("CinemaNVS.DAL.Database.Entities.Users.Login", b =>
                {
                    b.HasOne("CinemaNVS.DAL.Database.Entities.Users.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("CinemaNVS.DAL.Database.Entities.Movies.Actor", b =>
                {
                    b.Navigation("MovieActor");
                });

            modelBuilder.Entity("CinemaNVS.DAL.Database.Entities.Movies.Director", b =>
                {
                    b.Navigation("Movies");
                });

            modelBuilder.Entity("CinemaNVS.DAL.Database.Entities.Movies.Movie", b =>
                {
                    b.Navigation("MovieActor");

                    b.Navigation("Showings");
                });

            modelBuilder.Entity("CinemaNVS.DAL.Database.Entities.Transactions.Booking", b =>
                {
                    b.Navigation("BookingSeating");
                });

            modelBuilder.Entity("CinemaNVS.DAL.Database.Entities.Transactions.Seating", b =>
                {
                    b.Navigation("BookingSeating");
                });

            modelBuilder.Entity("CinemaNVS.DAL.Database.Entities.Transactions.Showing", b =>
                {
                    b.Navigation("Bookings");
                });

            modelBuilder.Entity("CinemaNVS.DAL.Database.Entities.Users.Customer", b =>
                {
                    b.Navigation("Bookings");
                });
#pragma warning restore 612, 618
        }
    }
}
