﻿// <auto-generated />
using System;
using CinemaNVS.DAL.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CinemaNVS.DAL.Migrations
{
    [DbContext(typeof(CinemaDBContext))]
    [Migration("20220602142015_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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
                            ImdbLink = "https://www.imdb.com/name/nm0004937/",
                            Name = "Jamie Foxx"
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
                            ImdbLink = "https://www.imdb.com/name/nm0000233/",
                            Name = "Quentin Tarantino"
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

                    b.Property<decimal>("Rating")
                        .HasColumnType("decimal(2,1)");

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
                            ImdbLink = "https://www.imdb.com/title/tt1853728/",
                            IsRunning = 0,
                            Rating = 8.4m,
                            ReleaseDate = new DateTime(2013, 1, 24, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            RuntimeMinutes = 165,
                            Title = "Django Unchained",
                            TrailerLink = "https://www.youtube.com/watch?v=0fUCuvNlOCg"
                        });
                });

            modelBuilder.Entity("CinemaNVS.DAL.Database.Entities.Movies.MovieActor", b =>
                {
                    b.Property<int>("MovieId")
                        .HasColumnType("int");

                    b.Property<int>("ActorId")
                        .HasColumnType("int");

                    b.HasKey("MovieId", "ActorId");

                    b.HasIndex("ActorId");

                    b.ToTable("MovieActor");

                    b.HasData(
                        new
                        {
                            MovieId = 1,
                            ActorId = 1
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

                    b.Property<int>("MovieId")
                        .HasColumnType("int");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("MovieId");

                    b.ToTable("Bookings");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BookingDate = new DateTime(2022, 5, 24, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CustomerId = 1,
                            MovieId = 1,
                            Price = 140
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

                    b.Property<int?>("CustomerId")
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
                            CustomerId = 1,
                            IsAdmin = "no",
                            Password = "Passw0rd",
                            Username = "Bobby"
                        },
                        new
                        {
                            Id = 2,
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

                    b.HasOne("CinemaNVS.DAL.Database.Entities.Movies.Movie", "Movie")
                        .WithMany()
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Movie");
                });

            modelBuilder.Entity("CinemaNVS.DAL.Database.Entities.Users.Login", b =>
                {
                    b.HasOne("CinemaNVS.DAL.Database.Entities.Users.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId");

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
                });

            modelBuilder.Entity("CinemaNVS.DAL.Database.Entities.Users.Customer", b =>
                {
                    b.Navigation("Bookings");
                });
#pragma warning restore 612, 618
        }
    }
}
