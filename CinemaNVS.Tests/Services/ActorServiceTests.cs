using CinemaNVS.DAL.Database.Entities.Movies;
using CinemaNVS.DAL.Repositories.Movies;
using CinemasNVS.BLL.DTOs;
using CinemasNVS.BLL.Services.MovieServices;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace CinemaNVS.Tests.Services
{
    public class ActorServiceTests
    {
        private readonly Mock<IActorRepository> _actorRepositoryMock = new Mock<IActorRepository>();
        private readonly IActorService _actorService;

        public ActorServiceTests()
        {
            _actorService = new ActorService(_actorRepositoryMock.Object);
        }

        [Fact]
        public async void GetAllActorsAsync_ShouldReturnListOfActorResponses_WhenActorsExist()
        {
            //Arrange
            _actorRepositoryMock
                .Setup(x => x.SelectAllActorsAsync())
                .ReturnsAsync(ActorList());

            //Act
            var result = (List<ActorResponse>)await _actorService.GetAllActorsAsync();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<List<ActorResponse>>(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async void GetAllActorsAsync_ShouldReturnEmptyListOfActorResponses_WhenNoActorsExist()
        {
            //Arrange
            IEnumerable<Actor> emptyActorList = new List<Actor>();

            _actorRepositoryMock
                .Setup(x => x.SelectAllActorsAsync())
                .ReturnsAsync(emptyActorList);

            //Act
            var result = (List<ActorResponse>)await _actorService.GetAllActorsAsync();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<List<ActorResponse>>(result);
            Assert.Empty(result);
        }

        [Fact]
        public async void GetActorByIdAsync_ShouldReturnActorResponse_WhenActorExists()
        {
            //Arrange
            int actorId = 1;

            _actorRepositoryMock
                .Setup(x => x.SelectActorByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(Actor());

            //Act
            var result = await _actorService.GetActorByIdAsync(actorId);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<ActorResponse>(result);
            Assert.Equal(actorId, result.Id);
        }

        [Fact]
        public async void GetActorByIdAsync_ShouldReturnNull_WhenActorDoesNotExist()
        {
            //Arrange
            int actorId = 1;

            _actorRepositoryMock
                .Setup(x => x.SelectActorByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            //Act
            var result = await _actorService.GetActorByIdAsync(actorId);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async void UpdateActorByIdAsync_ShouldReturnActorResponse_WhenActorIsSuccessfullyUpdated()
        {
            //Arrange
            int actorId = 1;

            _actorRepositoryMock
                .Setup(x => x.UpdateActorByIdAsync(It.IsAny<Actor>(), It.IsAny<int>()))
                .ReturnsAsync(Actor());

            //Act
            var result = await _actorService.UpdateActorByIdAsync(actorId, ActorRequest());

            //Assert
            Assert.NotNull(result);
            Assert.IsType<ActorResponse>(result);
            Assert.Equal(actorId, result.Id);
        }

        [Fact]
        public async void UpdateActorByIdAsync_ShouldReturnNull_WhenActorIsNotUpdated()
        {
            //Arrange
            int actorId = 1;

            _actorRepositoryMock
                .Setup(x => x.UpdateActorByIdAsync(It.IsAny<Actor>(), It.IsAny<int>()))
                .ReturnsAsync(() => null);

            //Act
            var result = await _actorService.UpdateActorByIdAsync(actorId, ActorRequest());

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async void CreateActorAsync_ShouldReturnActorResponse_WhenActorIsSuccessfullyCreated()
        {
            //Arrange
            _actorRepositoryMock
                .Setup(x => x.InsertActorAsync(It.IsAny<Actor>()))
                .ReturnsAsync(Actor());

            //Act
            var result = await _actorService.CreateActorAsync(ActorRequest());

            //Assert
            Assert.NotNull(result);
            Assert.IsType<ActorResponse>(result);
            Assert.Equal("Test Name", result.Name);
        }

        [Fact]
        public async void CreateActorAsync_ShouldReturnNull_WhenActorIsNotCreated()
        {
            //Arrange
            _actorRepositoryMock
                .Setup(x => x.InsertActorAsync(It.IsAny<Actor>()))
                .ReturnsAsync(() => null);

            //Act
            var result = await _actorService.CreateActorAsync(ActorRequest());

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async void DeleteActorById_ShouldReturnActorResponse_WhenActorIsSuccessfullyDeleted()
        {
            //Arrange
            int actorId = 1;

            _actorRepositoryMock
                .Setup(x => x.DeleteActorByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(Actor());

            //Act
            var result = await _actorService.DeleteActorByIdAsync(actorId);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<ActorResponse>(result);
            Assert.Equal(actorId, result.Id);
        }


        [Fact]
        public async void DeleteActorById_ShouldReturnNull_WhenActorIsNotDeleted()
        {
            //Arrange
            int actorId = 1;

            _actorRepositoryMock
                .Setup(x => x.DeleteActorByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            //Act
            var result = await _actorService.DeleteActorByIdAsync(actorId);

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
                    MovieActor = new List<MovieActor>()
                },
                new Actor()
                {
                    Id = 2,
                    Name = "Test Name2",
                    ImdbLink = "imdblink2.dk",
                    MovieActor = new List<MovieActor>()
                }
            };
        }

        private ActorRequest ActorRequest()
        {
            return new ActorRequest()
            {
                Name = "Test Name",
                ImdbLink = "imdblink.dk"
            };
        }

        private Actor Actor()
        {
            return new Actor()
            {
                Id = 1,
                Name = "Test Name",
                ImdbLink = "imdblink.dk",
                MovieActor = new List<MovieActor>()
            };
        }
    }
}
