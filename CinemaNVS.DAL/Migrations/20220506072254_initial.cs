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
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    Rating = table.Column<decimal>(type: "decimal(2,1)", nullable: false),
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
                name: "ActorMovie",
                columns: table => new
                {
                    ActorsId = table.Column<int>(type: "int", nullable: false),
                    MoviesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActorMovie", x => new { x.ActorsId, x.MoviesId });
                    table.ForeignKey(
                        name: "FK_ActorMovie_Actors_ActorsId",
                        column: x => x.ActorsId,
                        principalTable: "Actors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActorMovie_Movies_MoviesId",
                        column: x => x.MoviesId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Actors",
                columns: new[] { "Id", "ImdbLink", "Name" },
                values: new object[] { 1, "https://www.imdb.com/name/nm0004937/", "Jamie Foxx" });

            migrationBuilder.InsertData(
                table: "Directors",
                columns: new[] { "Id", "ImdbLink", "Name" },
                values: new object[] { 1, "https://www.imdb.com/name/nm0000233/", "Quentin Tarantino" });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "DirectorId", "ImdbLink", "IsRunning", "Rating", "ReleaseDate", "RuntimeMinutes", "Title", "TrailerLink" },
                values: new object[] { 1, 1, "https://www.imdb.com/title/tt1853728/", 0, 8.4m, new DateTime(2013, 1, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 165, "Django Unchained", "https://www.youtube.com/watch?v=0fUCuvNlOCg" });

            migrationBuilder.InsertData(
                table: "ActorMovie",
                columns: new[] { "ActorsId", "MoviesId" },
                values: new object[] { 1, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_ActorMovie_MoviesId",
                table: "ActorMovie",
                column: "MoviesId");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_DirectorId",
                table: "Movies",
                column: "DirectorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActorMovie");

            migrationBuilder.DropTable(
                name: "Actors");

            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropTable(
                name: "Directors");
        }
    }
}
