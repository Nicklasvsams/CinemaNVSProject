using CinemaNVS.DAL.Database;
using CinemaNVS.DAL.Database.Entities.Movies;
using CinemaNVS.DAL.Repositories.Movies;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace CinemaNVS.Tests.Repository
{
    public class MovieRepositoryTests
    {
        private readonly CinemaDBContext _dbContext;
        private readonly IMovieRepository _movieRepository;

        public MovieRepositoryTests()
        {
            DbContextOptions<CinemaDBContext> _dbContextOptions = new DbContextOptionsBuilder<CinemaDBContext>()
                .UseInMemoryDatabase(databaseName: "MovieRepositoryDB")
                .Options;
            _dbContext = new CinemaDBContext(_dbContextOptions);
            _movieRepository = new MovieRepository(_dbContext); 
        }

        [Fact]
        public async void SelectAllMoviesAsync_ShouldReturnListOfMovies_WhenMoviesExist()
        {
            //Arrange
            await _dbContext.Database.EnsureDeletedAsync();
            await _dbContext.Movies.AddRangeAsync(MovieList());
            await _dbContext.SaveChangesAsync();

            //Act
            var result = (List<Movie>)await _movieRepository.SelectAllMoviesAsync();

            //Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.IsType<List<Movie>>(result);
        }

        [Fact]
        public async void SelectAllMoviesAsync_ShouldReturnEmptyListOfMovies_WhenNoMoviesExist()
        {
            //Arrange
            await _dbContext.Database.EnsureDeletedAsync();
            await _dbContext.SaveChangesAsync();

            //Act
            var result = (List<Movie>)await _movieRepository.SelectAllMoviesAsync();

            //Assert
            Assert.NotNull(result);
            Assert.Empty(result);
            Assert.IsType<List<Movie>>(result);
        }

        [Fact]
        public async void SelectMovieByIdAsync_ShouldReturnSingleMovie_IfMovieExists()
        {
            //Arrange
            int movieId = 1;

            await _dbContext.Database.EnsureDeletedAsync();

            _dbContext.Movies.Add(Movie());

            await _dbContext.SaveChangesAsync();

            //Act
            var result = await _movieRepository.SelectMovieByIdAsync(movieId);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Movie>(result);
            Assert.Equal(movieId, result.Id);
            Assert.Equal(191, result.RuntimeMinutes);
        }

        [Fact]
        public async void SelectMovieByIdAsync_ShouldReturnNull_WhenMovieDoesNotExist()
        {
            //Arrange
            int movieId = 1;

            await _dbContext.Database.EnsureDeletedAsync();

            //Act
            var result = await _movieRepository.SelectMovieByIdAsync(movieId);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async void DeleteMovieByIdAsync_ShouldReturnDeletedMovie_WhenMovieExists()
        {
            //Arrange
            int movieId = 1;

            await _dbContext.Database.EnsureDeletedAsync();

            _dbContext.Movies.Add(Movie());

            await _dbContext.SaveChangesAsync();

            //Act
            var result = await _movieRepository.DeleteMovieByIdAsync(movieId);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(movieId, result.Id);
            Assert.IsType<Movie>(result);
        }

        [Fact]
        public async void DeleteMovieByIdAsync_ShouldReturnMull_WhenMovieDoesNotExist()
        {
            //Arrange
            int movieId = 1;

            await _dbContext.Database.EnsureDeletedAsync();

            //Act
            var result = await _movieRepository.DeleteMovieByIdAsync(movieId);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async void InsertMovieAsync_ShouldAddIdAndReturnMovie_WhenMovieIsInsertedSuccessfully()
        {
            //Arrange
            await _dbContext.Database.EnsureDeletedAsync();

            Movie movie = new Movie()
            {
                Title = "Test",
                IsRunning = 1,
                ReleaseDate = DateTime.Now,
                ImdbLink = "TestLink",
                RuntimeMinutes = 191,
                TrailerLink = "TestLink",
                DirectorId = 1,
                Director = new Director(),
                MovieActor = new List<MovieActor>()
            };

            //Act
            var result = await _movieRepository.InsertMovieAsync(movie);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Movie>(result);
            Assert.Equal("Test", result.Title);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async void InsertMovieAsync_ShouldFailToAddMovie_WhenMovieWithSameIdAlreadyExists()
        {
            //Arrange
            await _dbContext.Database.EnsureDeletedAsync();

            Movie movie = new Movie()
            {
                Id = 1,
                Title = "Test",
                IsRunning = 1,
                ReleaseDate = DateTime.Now,
                ImdbLink = "TestLink",
                RuntimeMinutes = 191,
                TrailerLink = "TestLink",
                DirectorId = 1,
                Director = new Director(),
                MovieActor = new List<MovieActor>()
            };

            _dbContext.Movies.Add(movie);

            await _dbContext.SaveChangesAsync();

            //Act
            async Task action() => await _movieRepository.InsertMovieAsync(movie);
            var ex = await Assert.ThrowsAsync<ArgumentException>(action);

            //Assert
            Assert.Contains("An item with the same key has already been added", ex.Message);
        }

        [Fact]
        public async void UpdateMovieAsync_ShouldReturnMovie_WhenMovieIsSuccessfullyUpdated()
        {
            //Arrange
            int movieId = 1;

            await _dbContext.Database.EnsureDeletedAsync();

            _dbContext.Movies.Add(Movie());

            await _dbContext.SaveChangesAsync();

            Movie movie = new Movie()
            {
                Title = "TestUpdate",
                TrailerLink = "UpdateLinkTrailer",
                ReleaseDate = new DateTime(1991, 06, 28),
                ImdbLink = "UpdateLinkImdb",
                IsRunning = 0,
                RuntimeMinutes = 50,
                DirectorId = 3,
                MovieActor = new List<MovieActor>(),
                Director = new Director()
            };

            //Act
            var result = await _movieRepository.UpdateMovieByIdAsync(movie, movieId);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Movie>(result);
            Assert.Equal(movieId, result.Id);
            Assert.Equal("TestUpdate", result.Title);
            Assert.Equal("UpdateLinkTrailer", result.TrailerLink);
            Assert.Equal("UpdateLinkImdb", result.ImdbLink);
            Assert.Equal(0, result.IsRunning);
            Assert.Equal(50, result.RuntimeMinutes);
            Assert.Equal(3, result.DirectorId);
            Assert.Equal(new DateTime(1991, 06, 28), result.ReleaseDate);
        }

        [Fact]
        public async Task UpdateMovieById_ShouldReturnNull_WhenMovieToUpdateDoesNotExist()
        {
            //Arrange
            int movieId = 1;

            await _dbContext.Database.EnsureDeletedAsync();

            Movie movie = new Movie()
            {
                Title = "TestUpdate",
                TrailerLink = "UpdateLinkTrailer",
                ReleaseDate = new DateTime(1991, 06, 28),
                ImdbLink = "UpdateLinkImdb",
                IsRunning = 0,
                RuntimeMinutes = 50,
                DirectorId = 3,
                MovieActor = new List<MovieActor>(),
                Director = new Director()
            };

            //Act
            var result = await _movieRepository.UpdateMovieByIdAsync(movie, movieId);

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
                    DirectorId = 2,
                    Director = new Director(),
                    MovieActor = new List<MovieActor>()
                }
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
                RuntimeMinutes = 191,
                TrailerLink = "TestLink",
                DirectorId = 1,
                Director = new Director(),
                MovieActor = new List<MovieActor>()
            };
        }
    }
}
