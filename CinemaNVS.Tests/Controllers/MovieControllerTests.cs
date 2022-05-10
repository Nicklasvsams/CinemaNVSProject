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

            movies.AddRange(MovieResponseList());

            _mockMovieService.Setup(x => x.GetAllMoviesAsync())
                .ReturnsAsync(movies);

            // Act
            var result = await _movieController.GetAll();
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode204_WhenNoMoviesExist()
        {
            // Arrange
            List<MovieResponse> movies = new List<MovieResponse>();

            _mockMovieService.Setup(x => x.GetAllMoviesAsync())
                .ReturnsAsync(movies);

            // Act
            var result = await _movieController.GetAll();
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(204, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode500_WhenServiceReturnsNull()
        {
            // Arrange
            List<MovieResponse> games = new List<MovieResponse>();

            _mockMovieService
                .Setup(x => x.GetAllMoviesAsync())
                .ReturnsAsync(() => null);

            // Act
            var result = await _movieController.GetAll();
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            _mockMovieService
                .Setup(x => x.GetAllMoviesAsync())
                .ReturnsAsync(() => throw new Exception("test"));

            // Act
            var result = await _movieController.GetAll();
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetById_ShouldReturnStatusCode200_WhenMovieExists()
        {
            // Arrange
            int movieId = 1;

            _mockMovieService
                .Setup(x => x.GetMovieByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(MovieResponse());

            // Act
            var result = await _movieController.GetById(movieId);
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetById_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            int movieId = 1;

            _mockMovieService
                .Setup(x => x.GetMovieByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(() => throw new Exception("Test"));

            // Act
            var result = await _movieController.GetById(movieId);
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetById_ShouldReturnStatusCode404_WhenMovieDoesNotExist()
        {
            // Arrange
            int movieId = 1;

            _mockMovieService
                .Setup(x => x.GetMovieByIdAsync(movieId))
                .ReturnsAsync(() => null);

            // Act
            var result = await _movieController.GetById(movieId);
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(404, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Create_ShouldReturnStatusCode200_WhenMovieIsCreated()
        {
            // Arrange
            _mockMovieService
                .Setup(x => x.CreateMovieAsync(It.IsAny<MovieRequest>()))
                .ReturnsAsync(MovieResponse());

            // Act
            var result = await _movieController.Create(MovieRequest());
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Create_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            _mockMovieService
                .Setup(x => x.CreateMovieAsync(It.IsAny<MovieRequest>()))
                .ReturnsAsync(() => throw new Exception("Test"));

            // Act
            var result = await _movieController.Create(MovieRequest());
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Update_ShouldReturnStatusCode200_WhenUpdateIsSuccessful()
        {
            // Arrange
            int movieId = 1;

            _mockMovieService
                .Setup(x => x.UpdateMovieByIdAsync(It.IsAny<MovieRequest>(), It.IsAny<int>()))
                .ReturnsAsync(MovieResponse());

            // Act
            var result = await _movieController.Update(movieId, MovieRequest());
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Update_ShouldReturnStatusCode404_WhenMovieToUpdateIsNotFound()
        {
            // Arrange
            int movieId = 1;

            _mockMovieService
                .Setup(x => x.UpdateMovieByIdAsync(It.IsAny<MovieRequest>(), It.IsAny<int>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _movieController.Update(movieId, MovieRequest());
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(404, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Update_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            int movieId = 1;

            _mockMovieService
                .Setup(x => x.UpdateMovieByIdAsync(It.IsAny<MovieRequest>(), It.IsAny<int>()))
                .ReturnsAsync(() => throw new Exception("Test"));

            // Act
            var result = await _movieController.Update(movieId, MovieRequest());
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Delete_ShouldReturnStatusCode200_WhenDeleteIsSuccessful()
        {
            // Arrange
            int movieId = 1;

            _mockMovieService
                .Setup(x => x.DeleteMovieByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(MovieResponse());

            // Act
            var result = await _movieController.Delete(movieId);
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Delete_ShouldReturnStatusCode404_WhenMovieToDeleteIsNotFound()
        {
            // Arrange
            int movieId = 1;

            _mockMovieService
                .Setup(x => x.DeleteMovieByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _movieController.Delete(movieId);
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(404, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Delete_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            int movieId = 1;

            _mockMovieService
                .Setup(x => x.DeleteMovieByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(() => throw new Exception("Test"));

            // Act
            var result = await _movieController.Delete(movieId);
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        private IEnumerable<MovieResponse> MovieResponseList()
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

        private MovieResponse MovieResponse()
        {
            return new MovieResponse()
            {
                Id = 1,
                Title = "Test",
                IsRunning = true,
                ReleaseDate = DateTime.Now,
                ImdbLink = "TestLink",
                RuntimeMinutes = 191,
                TrailerLink = "TestLink",
                Rating = 1,
                DirectorId = 1,
                DirectorResponse = new MovieDirectorResponse(),
                ActorResponse = new List<MovieActorResponse>()
            };
        }

        private MovieRequest MovieRequest()
        {
            return new MovieRequest()
            {
                Title = "Test",
                TrailerLink = "TestLink",
                ImdbLink = "TestLink",
                ReleaseDate = DateTime.Now,
                IsRunning = true,
                Rating = 5,
                RuntimeMinutes = 29,
                DirectorId = 1
            };
        }
    }
}
