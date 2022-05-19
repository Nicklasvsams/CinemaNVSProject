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
    public class ActorRepositoryTests
    {
        private readonly CinemaDBContext _dbContext;
        private readonly IActorRepository _actorRepository;

        public ActorRepositoryTests()
        {
            DbContextOptions<CinemaDBContext> _dbContextOptions = new DbContextOptionsBuilder<CinemaDBContext>()
                .UseInMemoryDatabase(databaseName: "DirectorRepositoryDB")
                .Options;
            _dbContext = new CinemaDBContext(_dbContextOptions);
            _actorRepository = new ActorRepository(_dbContext);
        }

        [Fact]
        public async void SelectAllActorsAsync_ShouldReturnListOfActors_WhenActorsExist()
        {
            //Arrange
            await _dbContext.Database.EnsureDeletedAsync();
            await _dbContext.Actors.AddRangeAsync(ActorList());
            await _dbContext.SaveChangesAsync();

            //Act
            var result = (List<Actor>)await _actorRepository.SelectAllActorsAsync();

            //Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.IsType<List<Actor>>(result);
        }

        [Fact]
        public async void SelectAllActorsAsync_ShouldReturnEmptyListOfActors_WhenNoActorsExist()
        {
            //Arrange
            await _dbContext.Database.EnsureDeletedAsync();
            await _dbContext.SaveChangesAsync();

            //Act
            var result = (List<Actor>)await _actorRepository.SelectAllActorsAsync();

            //Assert
            Assert.NotNull(result);
            Assert.Empty(result);
            Assert.IsType<List<Actor>>(result);
        }

        [Fact]
        public async void SelectActorByIdAsync_ShouldReturnSingleActor_IfActorExists()
        {
            //Arrange
            int actorId = 1;

            await _dbContext.Database.EnsureDeletedAsync();

            _dbContext.Actors.Add(Actor());

            await _dbContext.SaveChangesAsync();

            //Act
            var result = await _actorRepository.SelectActorByIdAsync(actorId);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Actor>(result);
            Assert.Equal(actorId, result.Id);
            Assert.Equal("Test Name", result.Name);
        }

        [Fact]
        public async void SelectActorByIdAsync_ShouldReturnNull_WhenActorDoesNotExist()
        {
            //Arrange
            int directorId = 1;

            await _dbContext.Database.EnsureDeletedAsync();

            //Act
            var result = await _actorRepository.SelectActorByIdAsync(directorId);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async void DeleteActorByIdAsync_ShouldReturnDeletedActor_WhenActorExists()
        {
            //Arrange
            int actorId = 1;

            await _dbContext.Database.EnsureDeletedAsync();

            _dbContext.Actors.Add(Actor());

            await _dbContext.SaveChangesAsync();

            //Act
            var result = await _actorRepository.DeleteActorByIdAsync(actorId);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(actorId, result.Id);
            Assert.IsType<Actor>(result);
        }

        [Fact]
        public async void DeleteActorByIdAsync_ShouldReturnMull_WhenActorDoesNotExist()
        {
            //Arrange
            int actorId = 1;

            await _dbContext.Database.EnsureDeletedAsync();

            //Act
            var result = await _actorRepository.DeleteActorByIdAsync(actorId);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async void InsertActorAsync_ShouldAddIdAndReturnActor_WhenActorIsInsertedSuccessfully()
        {
            //Arrange
            await _dbContext.Database.EnsureDeletedAsync();

            Actor actor = new Actor()
            {
                Name = "Test Name",
                ImdbLink = "imdblink.dk",
                Movies = new List<Movie>()
            };

            //Act
            var result = await _actorRepository.InsertActorAsync(actor);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Actor>(result);
            Assert.Equal("Test Name", result.Name);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async void InsertActorAsync_ShouldFailToAddActor_WhenActorWithSameIdAlreadyExists()
        {
            //Arrange
            await _dbContext.Database.EnsureDeletedAsync();

            Actor actor = new Actor()
            {
                Id = 1,
                Name = "Test Name",
                ImdbLink = "imdblink.dk",
                Movies = new List<Movie>()
            };

            _dbContext.Actors.Add(actor);

            await _dbContext.SaveChangesAsync();

            //Act
            async Task action() => await _actorRepository.InsertActorAsync(actor);
            var ex = await Assert.ThrowsAsync<ArgumentException>(action);

            //Assert
            Assert.Contains("An item with the same key has already been added", ex.Message);
        }

        [Fact]
        public async void UpdateActorAsync_ShouldReturnActor_WhenActorIsSuccessfullyUpdated()
        {
            //Arrange
            int actorId = 1;

            await _dbContext.Database.EnsureDeletedAsync();

            _dbContext.Actors.Add(Actor());

            await _dbContext.SaveChangesAsync();

            Actor actor = new Actor()
            {
                Name = "Test Updatename",
                ImdbLink = "imdblinkupdate.dk",
                Movies = new List<Movie>()
            };

            //Act
            var result = await _actorRepository.UpdateActorByIdAsync(actor, actorId);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Actor>(result);
            Assert.Equal(actorId, result.Id);
            Assert.Equal("Test Updatename", result.Name);
            Assert.Equal("imdblinkupdate.dk", result.ImdbLink);
        }

        [Fact]
        public async Task UpdateActorById_ShouldReturnNull_WhenActorToUpdateDoesNotExist()
        {
            //Arrange
            int actorId = 1;

            await _dbContext.Database.EnsureDeletedAsync();

            Actor actor = new Actor()
            {
                Name = "Test Updatename",
                ImdbLink = "imdblinkupdate.dk",
                Movies = new List<Movie>()
            };

            //Act
            var result = await _actorRepository.UpdateActorByIdAsync(actor, actorId);

            //Assert
            Assert.Null(result);
        }

        private List<Actor> ActorList()
        {
            return new List<Actor>()
            {
                new Actor()
                {
                    Id = 1,
                    Name = "Test Name",
                    ImdbLink = "imdblink.dk",
                    Movies = new List<Movie>()
                },
                new Actor()
                {
                    Id = 2,
                    Name = "Test Name2",
                    ImdbLink = "imdblink2.dk",
                    Movies = new List<Movie>()
                }
            };
        }

        private Actor Actor()
        {
            return new Actor()
            {
                Id = 1,
                Name = "Test Name",
                ImdbLink = "imdblink.dk",
                Movies = new List<Movie>()
            };
        }
    }
}
