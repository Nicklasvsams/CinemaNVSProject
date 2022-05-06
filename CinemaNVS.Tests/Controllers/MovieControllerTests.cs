using CinemaNVS.Controllers;
using CinemasNVS.BLL.DTOs;
using CinemasNVS.BLL.Services.MovieServices;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace CinemaNVS.Tests.Controllers
{
    public class MovieControllerTests
    {
        private readonly Mock<IMovieService> _mockMovieService = new Mock<IMovieService>();
        private readonly MovieController _movieController;

        public MovieControllerTests()
        {
            _movieController = new MovieController(_mockMovieService.Object);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode200_WhenMoviesExist()
        {
            // Arrange
            List<MovieResponse> movies = new List<MovieResponse>();

            movies.AddRange(MovieList());

            _mockMovieService.Setup(x => x.GetAllMoviesAsync())
                .ReturnsAsync(movies);

            // Act
            var result = await _movieController.GetAll();
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(200, statusCodeResult.StatusCode);

        }

        private IEnumerable<MovieResponse> MovieList()
        {
            return new List<MovieResponse>()
            {
                new MovieResponse()
                {
                    Id = 1,
                    Title = "Test",
                    ImdbLink = "Test",
                    IsRunning = false,
                    Rating = 10,
                    ReleaseDate = DateTime.Now,
                    RuntimeMinutes = 10,
                    TrailerLink = "Test",
                    DirectorId = 1
                },
                new MovieResponse()
                {
                    Id = 2,
                    Title = "Test",
                    ImdbLink = "Test",
                    IsRunning = false,
                    Rating = 20,
                    ReleaseDate = DateTime.Now,
                    RuntimeMinutes = 20,
                    TrailerLink = "Test",
                    DirectorId = 2
                }
            };
        }
    }
}
