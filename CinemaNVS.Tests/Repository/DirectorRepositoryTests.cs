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
    public class DirectorRepositoryTests
    {
        private readonly CinemaDBContext _dbContext;
        private readonly IDirectorRepository _directorRepository;

        public DirectorRepositoryTests()
        {
            DbContextOptions<CinemaDBContext> _dbContextOptions = new DbContextOptionsBuilder<CinemaDBContext>()
                .UseInMemoryDatabase(databaseName: "DirectorRepositoryDB")
                .Options;
            _dbContext = new CinemaDBContext(_dbContextOptions);
            _directorRepository = new DirectorRepository(_dbContext);
        }

        [Fact]
        public async void SelectAllDirectorsAsync_ShouldReturnListOfDirectors_WhenDirectorsExist()
        {
            //Arrange
            await _dbContext.Database.EnsureDeletedAsync();
            await _dbContext.Directors.AddRangeAsync(DirectorList());
            await _dbContext.SaveChangesAsync();

            //Act
            var result = (List<Director>)await _directorRepository.SelectAllDirectorsAsync();

            //Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.IsType<List<Director>>(result);
        }

        [Fact]
        public async void SelectAllDirectorsAsync_ShouldReturnEmptyListOfDirectors_WhenNoDirectorsExist()
        {
            //Arrange
            await _dbContext.Database.EnsureDeletedAsync();
            await _dbContext.SaveChangesAsync();

            //Act
            var result = (List<Director>)await _directorRepository.SelectAllDirectorsAsync();

            //Assert
            Assert.NotNull(result);
            Assert.Empty(result);
            Assert.IsType<List<Director>>(result);
        }

        [Fact]
        public async void SelectDirectorByIdAsync_ShouldReturnSingleDirector_IfDirectorExists()
        {
            //Arrange
            int directorId = 1;

            await _dbContext.Database.EnsureDeletedAsync();

            _dbContext.Directors.Add(Director());

            await _dbContext.SaveChangesAsync();

            //Act
            var result = await _directorRepository.SelectDirectorByIdAsync(directorId);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Director>(result);
            Assert.Equal(directorId, result.Id);
            Assert.Equal("Test Name", result.Name);
        }

        [Fact]
        public async void SelectDirectorByIdAsync_ShouldReturnNull_WhenDirectorDoesNotExist()
        {
            //Arrange
            int directorId = 1;

            await _dbContext.Database.EnsureDeletedAsync();

            //Act
            var result = await _directorRepository.SelectDirectorByIdAsync(directorId);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async void DeleteDirectorByIdAsync_ShouldReturnDeletedDirector_WhenDirectorExists()
        {
            //Arrange
            int directorId = 1;

            await _dbContext.Database.EnsureDeletedAsync();

            _dbContext.Directors.Add(Director());

            await _dbContext.SaveChangesAsync();

            //Act
            var result = await _directorRepository.DeleteDirectorByIdAsync(directorId);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(directorId, result.Id);
            Assert.IsType<Director>(result);
        }

        [Fact]
        public async void DeleteDirectorByIdAsync_ShouldReturnMull_WhenDirectorDoesNotExist()
        {
            //Arrange
            int directorId = 1;

            await _dbContext.Database.EnsureDeletedAsync();

            //Act
            var result = await _directorRepository.DeleteDirectorByIdAsync(directorId);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async void InsertDirectorAsync_ShouldAddIdAndReturnDirector_WhenDirectorIsInsertedSuccessfully()
        {
            //Arrange
            await _dbContext.Database.EnsureDeletedAsync();

            Director director = new Director()
            {
                Name = "Test Name",
                ImdbLink = "imdblink.dk",
                Movies = new List<Movie>()
            };

            //Act
            var result = await _directorRepository.InsertDirectorAsync(director);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Director>(result);
            Assert.Equal("Test Name", result.Name);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async void InsertDirectorAsync_ShouldFailToAddDirector_WhenDirectorWithSameIdAlreadyExists()
        {
            //Arrange
            await _dbContext.Database.EnsureDeletedAsync();

            Director director = new Director()
            {
                Id = 1,
                Name = "Test Name",
                ImdbLink = "imdblink.dk",
                Movies = new List<Movie>()
            };

            _dbContext.Directors.Add(director);

            await _dbContext.SaveChangesAsync();

            //Act
            async Task action() => await _directorRepository.InsertDirectorAsync(director);
            var ex = await Assert.ThrowsAsync<ArgumentException>(action);

            //Assert
            Assert.Contains("An item with the same key has already been added", ex.Message);
        }

        [Fact]
        public async void UpdateDirectorAsync_ShouldReturnDirector_WhenDirectorIsSuccessfullyUpdated()
        {
            //Arrange
            int directorId = 1;

            await _dbContext.Database.EnsureDeletedAsync();

            _dbContext.Directors.Add(Director());

            await _dbContext.SaveChangesAsync();

            Director director = new Director()
            {
                Name = "Test Updatename",
                ImdbLink = "imdblinkupdate.dk",
                Movies = new List<Movie>()
            };

            //Act
            var result = await _directorRepository.UpdateDirectorByIdAsync(director, directorId);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Director>(result);
            Assert.Equal(directorId, result.Id);
            Assert.Equal("Test Updatename", result.Name);
            Assert.Equal("imdblinkupdate.dk", result.ImdbLink);
        }

        [Fact]
        public async Task UpdateDirectorById_ShouldReturnNull_WhenDirectorToUpdateDoesNotExist()
        {
            //Arrange
            int directorId = 1;

            await _dbContext.Database.EnsureDeletedAsync();

            Director director = new Director()
            {
                Name = "Test Updatename",
                ImdbLink = "imdblinkupdate.dk",
                Movies = new List<Movie>()
            };

            //Act
            var result = await _directorRepository.UpdateDirectorByIdAsync(director, directorId);

            //Assert
            Assert.Null(result);
        }

        private List<Director> DirectorList()
        {
            return new List<Director>()
            {
                new Director()
                {
                    Id = 1,
                    Name = "Test Name",
                    ImdbLink = "imdblink.dk",
                    Movies = new List<Movie>()
                },
                new Director()
                {
                    Id = 2,
                    Name = "Test Name2",
                    ImdbLink = "imdblink2.dk",
                    Movies = new List<Movie>()
                }
            };
        }

        private Director Director()
        {
            return new Director()
            {
                Id = 1,
                Name = "Test Name",
                ImdbLink = "imdblink.dk",
                Movies = new List<Movie>()
            };
        }
    }
}
