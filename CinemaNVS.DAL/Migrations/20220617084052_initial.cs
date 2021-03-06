using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CinemaNVS.DAL.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Actors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    ImdbLink = table.Column<string>(type: "nvarchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(30)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(30)", nullable: true),
                    PhoneNo = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    IsActive = table.Column<string>(type: "nvarchar(3)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Directors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    ImdbLink = table.Column<string>(type: "nvarchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Directors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Seatings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Seat = table.Column<string>(type: "nvarchar(3)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seatings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Logins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    IsAdmin = table.Column<string>(type: "nvarchar(3)", nullable: true),
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Logins_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    RuntimeMinutes = table.Column<int>(type: "int", nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "date", nullable: false),
                    IsRunning = table.Column<int>(type: "int", nullable: false),
                    TrailerLink = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    ImdbLink = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    DirectorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Movies_Directors_DirectorId",
                        column: x => x.DirectorId,
                        principalTable: "Directors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MovieActor",
                columns: table => new
                {
                    MovieId = table.Column<int>(type: "int", nullable: false),
                    ActorId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieActor", x => new { x.MovieId, x.ActorId });
                    table.ForeignKey(
                        name: "FK_MovieActor_Actors_ActorId",
                        column: x => x.ActorId,
                        principalTable: "Actors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieActor_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Showings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<int>(type: "int", nullable: false),
                    TimeOfShowing = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    MovieId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Showings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Showings_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookingDate = table.Column<DateTime>(type: "date", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    ShowingId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bookings_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bookings_Showings_ShowingId",
                        column: x => x.ShowingId,
                        principalTable: "Showings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookingSeating",
                columns: table => new
                {
                    BookingId = table.Column<int>(type: "int", nullable: false),
                    SeatingId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingSeating", x => new { x.BookingId, x.SeatingId });
                    table.ForeignKey(
                        name: "FK_BookingSeating_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookingSeating_Seatings_SeatingId",
                        column: x => x.SeatingId,
                        principalTable: "Seatings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Actors",
                columns: new[] { "Id", "ImdbLink", "Name" },
                values: new object[,]
                {
                    { 1, "https://www.imdb.com/name/nm0262635/", "Chris Evans" },
                    { 2, "https://www.imdb.com/name/nm1551130/", "Keke Palmer" },
                    { 3, "https://www.imdb.com/name/nm0812307/", "Peter Sohn" },
                    { 4, "https://www.imdb.com/name/nm0169806/", "Taiki Waititi" },
                    { 5, "https://www.imdb.com/name/nm0695435/", "Chris Pratt" },
                    { 6, "https://www.imdb.com/name/nm0397171/", "Bryce Dallas Howard" },
                    { 7, "https://www.imdb.com/name/nm0000368/", "Laura Dern" },
                    { 8, "https://www.imdb.com/name/nm0000554/", "Sam Neill" }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Email", "FirstName", "IsActive", "LastName", "PhoneNo" },
                values: new object[,]
                {
                    { 1, "Test@gmail.com", "admin", "yes", "admin", 51515151 },
                    { 2, "Test@gmail.com", "Bob", "yes", "Levinsen", 11223344 }
                });

            migrationBuilder.InsertData(
                table: "Directors",
                columns: new[] { "Id", "ImdbLink", "Name" },
                values: new object[,]
                {
                    { 2, "https://www.imdb.com/name/nm1119880/", "Colin Trevorrow" },
                    { 1, "https://www.imdb.com/name/nm0533691/", "Angus MacLane" }
                });

            migrationBuilder.InsertData(
                table: "Seatings",
                columns: new[] { "Id", "Seat" },
                values: new object[,]
                {
                    { 74, "H4" },
                    { 73, "H3" },
                    { 72, "H2" },
                    { 71, "H1" },
                    { 70, "G10" },
                    { 69, "G9" },
                    { 68, "G8" },
                    { 67, "G7" },
                    { 66, "G6" },
                    { 65, "G5" },
                    { 64, "G4" },
                    { 63, "G3" },
                    { 62, "G2" },
                    { 61, "G1" },
                    { 60, "F10" },
                    { 59, "F9" },
                    { 58, "F8" },
                    { 57, "F7" },
                    { 56, "F6" },
                    { 55, "F5" },
                    { 54, "F4" },
                    { 75, "H5" },
                    { 53, "F3" },
                    { 76, "H6" },
                    { 78, "H8" },
                    { 100, "J10" },
                    { 99, "J9" },
                    { 98, "J8" },
                    { 97, "J7" },
                    { 96, "J6" }
                });

            migrationBuilder.InsertData(
                table: "Seatings",
                columns: new[] { "Id", "Seat" },
                values: new object[,]
                {
                    { 95, "J5" },
                    { 94, "J4" },
                    { 93, "J3" },
                    { 92, "J2" },
                    { 91, "J1" },
                    { 77, "H7" },
                    { 90, "I10" },
                    { 88, "I8" },
                    { 87, "I7" },
                    { 86, "I6" },
                    { 85, "I5" },
                    { 84, "I4" },
                    { 83, "I3" },
                    { 82, "I2" },
                    { 81, "I1" },
                    { 80, "H10" },
                    { 79, "H9" },
                    { 89, "I9" },
                    { 1, "A1" },
                    { 51, "F1" },
                    { 50, "E10" },
                    { 22, "C2" },
                    { 21, "C1" },
                    { 20, "B10" },
                    { 19, "B9" },
                    { 18, "B8" },
                    { 17, "B7" },
                    { 16, "B6" },
                    { 15, "B5" },
                    { 14, "B4" },
                    { 13, "B3" },
                    { 12, "B2" },
                    { 11, "B1" },
                    { 10, "A10" },
                    { 9, "A9" },
                    { 8, "A8" },
                    { 7, "A7" },
                    { 6, "A6" },
                    { 5, "A5" },
                    { 4, "A4" },
                    { 3, "A3" },
                    { 2, "A2" }
                });

            migrationBuilder.InsertData(
                table: "Seatings",
                columns: new[] { "Id", "Seat" },
                values: new object[,]
                {
                    { 23, "C3" },
                    { 24, "C4" },
                    { 25, "C5" },
                    { 26, "C6" },
                    { 49, "E9" },
                    { 48, "E8" },
                    { 47, "E7" },
                    { 45, "E5" },
                    { 44, "E4" },
                    { 43, "E3" },
                    { 42, "E2" },
                    { 41, "E1" },
                    { 40, "D10" },
                    { 39, "D9" },
                    { 52, "F2" },
                    { 38, "D8" },
                    { 36, "D6" },
                    { 35, "D5" },
                    { 34, "D4" },
                    { 33, "D3" },
                    { 32, "D2" },
                    { 31, "D1" },
                    { 30, "C10" },
                    { 29, "C9" },
                    { 28, "C8" },
                    { 27, "C7" },
                    { 37, "D7" },
                    { 46, "E6" }
                });

            migrationBuilder.InsertData(
                table: "Logins",
                columns: new[] { "Id", "CustomerId", "IsAdmin", "Password", "Username" },
                values: new object[,]
                {
                    { 2, 1, "yes", "admin", "admin" },
                    { 1, 2, "no", "Passw0rd", "Bobby" }
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "DirectorId", "ImdbLink", "IsRunning", "ReleaseDate", "RuntimeMinutes", "Title", "TrailerLink" },
                values: new object[,]
                {
                    { 1, 1, "https://www.imdb.com/title/tt10298810/", 1, new DateTime(2022, 6, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 100, "Lightyear", "https://www.imdb.com/video/vi1034797593/?playlistId=tt10298810" },
                    { 2, 2, "https://www.imdb.com/title/tt8041270/", 1, new DateTime(2022, 6, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 146, "Jurassic World: Dominion", "https://www.imdb.com/video/vi764854809/?playlistId=tt8041270" }
                });

            migrationBuilder.InsertData(
                table: "MovieActor",
                columns: new[] { "ActorId", "MovieId", "Id" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 1, 2 },
                    { 3, 1, 3 },
                    { 4, 1, 4 },
                    { 5, 2, 5 },
                    { 6, 2, 6 },
                    { 7, 2, 5 },
                    { 8, 2, 5 }
                });

            migrationBuilder.InsertData(
                table: "Showings",
                columns: new[] { "Id", "MovieId", "Price", "TimeOfShowing" },
                values: new object[,]
                {
                    { 1, 1, 140, new DateTime(2022, 6, 28, 13, 30, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 1, 140, new DateTime(2022, 6, 28, 15, 30, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 1, 140, new DateTime(2022, 6, 28, 17, 30, 0, 0, DateTimeKind.Unspecified) },
                    { 4, 1, 140, new DateTime(2022, 6, 28, 19, 30, 0, 0, DateTimeKind.Unspecified) },
                    { 5, 2, 160, new DateTime(2022, 6, 27, 13, 30, 0, 0, DateTimeKind.Unspecified) },
                    { 6, 2, 160, new DateTime(2022, 6, 27, 15, 30, 0, 0, DateTimeKind.Unspecified) },
                    { 7, 2, 160, new DateTime(2022, 6, 27, 17, 30, 0, 0, DateTimeKind.Unspecified) },
                    { 8, 2, 160, new DateTime(2022, 6, 27, 19, 30, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "Id", "BookingDate", "CustomerId", "ShowingId" },
                values: new object[] { 1, new DateTime(2022, 5, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1 });

            migrationBuilder.InsertData(
                table: "BookingSeating",
                columns: new[] { "BookingId", "SeatingId", "Id" },
                values: new object[] { 1, 1, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_CustomerId",
                table: "Bookings",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_ShowingId",
                table: "Bookings",
                column: "ShowingId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingSeating_SeatingId",
                table: "BookingSeating",
                column: "SeatingId");

            migrationBuilder.CreateIndex(
                name: "IX_Logins_CustomerId",
                table: "Logins",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Logins_Username",
                table: "Logins",
                column: "Username",
                unique: true,
                filter: "[Username] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_MovieActor_ActorId",
                table: "MovieActor",
                column: "ActorId");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_DirectorId",
                table: "Movies",
                column: "DirectorId");

            migrationBuilder.CreateIndex(
                name: "IX_Showings_MovieId",
                table: "Showings",
                column: "MovieId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookingSeating");

            migrationBuilder.DropTable(
                name: "Logins");

            migrationBuilder.DropTable(
                name: "MovieActor");

            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "Seatings");

            migrationBuilder.DropTable(
                name: "Actors");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Showings");

            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropTable(
                name: "Directors");
        }
    }
}
