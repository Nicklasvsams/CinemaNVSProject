using CinemaNVS.DAL.Database.Entities.Movies;
using CinemaNVS.DAL.Repositories.Movies;
using CinemasNVS.BLL.DTOs;
using CinemasNVS.BLL.Services.MovieServices;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace CinemaNVS.Tests.Services
{
    public class MovieServiceTests
    {
        private readonly Mock<IMovieRepository> _movieRepositoryMock = new Mock<IMovieRepository>();
        private readonly IMovieService _movieService;

        public MovieServiceTests()
        {
            _movieService = new MovieService(_movieRepositoryMock.Object);
        }

        [Fact]
        public async void GetAllMoviesAsync_ShouldReturnListOfMovieResponses_WhenMoviesExist()
        {
            //Arrange
            _movieRepositoryMock
                .Setup(x => x.SelectAllMoviesAsync())
                .ReturnsAsync(MovieList());

            //Act
            var result = (List<MovieResponse>)await _movieService.GetAllMoviesAsync();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<List<MovieResponse>>(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async void GetAllMoviesAsync_ShouldReturnEmptyListOfMovieResponses_WhenNoMoviesExist()
        {
            //Arrange
            IEnumerable<Movie> emptyMovieList = new List<Movie>();

            _movieRepositoryMock
                .Setup(x => x.SelectAllMoviesAsync())
                .ReturnsAsync(emptyMovieList);

            //Act
            var result = (List<MovieResponse>)await _movieService.GetAllMoviesAsync();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<List<MovieResponse>>(result);
            Assert.Empty(result);
        }

        [Fact]
        public async void GetMovieByIdAsync_ShouldReturnMovieResponse_WhenMovieExists()
        {
            //Arrange
            int movieId = 1;

            _movieRepositoryMock
                .Setup(x => x.SelectMovieByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(Movie());

            //Act
            var result = await _movieService.GetMovieByIdAsync(movieId);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<MovieResponse>(result);
            Assert.Equal(movieId, result.Id);
        }

        [Fact]
        public async void GetMovieByIdAsync_ShouldReturnNull_WhenMovieDoesNotExist()
        {
            //Arrange
            int movieId = 1;

            _movieRepositoryMock
                .Setup(x => x.SelectMovieByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            //Act
            var result = await _movieService.GetMovieByIdAsync(movieId);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async void UpdateMovieByIdAsync_ShouldReturnMovieResponse_WhenMovieIsSuccessfullyUpdated()
        {
            //Arrange
            int movieId = 1;

            _movieRepositoryMock
                .Setup(x => x.UpdateMovieByIdAsync(It.IsAny<Movie>(), It.IsAny<int>()))
                .ReturnsAsync(Movie());

            //Act
            var result = await _movieService.UpdateMovieByIdAsync(MovieRequest(), movieId);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<MovieResponse>(result);
            Assert.Equal(movieId, result.Id);
        }

        [Fact]
        public async void UpdateMovieByIdAsync_ShouldReturnNull_WhenMovieIsNotUpdated()
        {
            //Arrange
            int movieId = 1;

            _movieRepositoryMock
                .Setup(x => x.UpdateMovieByIdAsync(It.IsAny<Movie>(), It.IsAny<int>()))
                .ReturnsAsync(() => null);

            //Act
            var result = await _movieService.UpdateMovieByIdAsync(MovieRequest(), movieId);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async void CreateMovieAsync_ShouldReturnMovieResponse_WhenMovieIsSuccessfullyCreated()
        {
            //Arrange
            _movieRepositoryMock
                .Setup(x => x.InsertMovieAsync(It.IsAny<Movie>()))
                .ReturnsAsync(Movie());

            //Act
            var result = await _movieService.CreateMovieAsync(MovieRequest());

            //Assert
            Assert.NotNull(result);
            Assert.IsType<MovieResponse>(result);
            Assert.Equal("Test", result.Title);
        }

        [Fact]
        public async void CreateMovieAsync_ShouldReturnNull_WhenMovieIsNotCreated()
        {
            //Arrange
            _movieRepositoryMock
                .Setup(x => x.InsertMovieAsync(It.IsAny<Movie>()))
                .ReturnsAsync(() => null);

            //Act
            var result = await _movieService.CreateMovieAsync(MovieRequest());

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async void DeleteMovieById_ShouldReturnMovieResponse_WhenMovieIsSuccessfullyDeleted()
        {
            //Arrange
            int movieId = 1;

            _movieRepositoryMock
                .Setup(x => x.DeleteMovieByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(Movie());

            //Act
            var result = await _movieService.DeleteMovieByIdAsync(movieId);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<MovieResponse>(result);
            Assert.Equal(movieId, result.Id);
        }


        [Fact]
        public async void DeleteMovieById_ShouldReturnNull_WhenMovieIsNotDeleted()
        {
            //Arrange
            int movieId = 1;

            _movieRepositoryMock
                .Setup(x => x.DeleteMovieByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            //Act
            var result = await _movieService.DeleteMovieByIdAsync(movieId);

            //Assert
            Assert.Null(result);
        }

        private List<Movie> MovieList()
        {
            return new List<Movie>()
            {
                new Movie()
                {
                    Id = 1,
                    Title = "Test",
                    IsRunning = 1,
                    ReleaseDate = DateTime.Now,
                    ImdbLink = "TestLink",
                    RuntimeMinutes = 191,
                    TrailerLink = "TestLink",
                    Rating = 1,
                    DirectorId = 1,
                    Director = new Director(),
                    MovieActor = new List<MovieActor>()
                },
                new Movie()
                {
                    Id = 2,
                    Title = "Test2",
                    IsRunning = 0,
                    ReleaseDate = DateTime.Now,
                    ImdbLink = "TestLink2",
                    RuntimeMinutes = 19,
                    TrailerLink = "TestLink2",
                    Rating = 2,
                    DirectorId = 2,
                    Director = new Director(),
                    MovieActor = new List<MovieActor>()
                }
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

        private Movie Movie()
        {
            return new Movie()
            {
                Id = 1,
                Title = "Test",
                IsRunning = 1,
                ReleaseDate = DateTime.Now,
                ImdbLink = "TestLink",
                RuntimeMinutes = 29,
                TrailerLink = "TestLink",
                Rating = 5,
                DirectorId = 1,
                Director = new Director(),
                MovieActor = new List<MovieActor>()
            };
        }
    }
}
