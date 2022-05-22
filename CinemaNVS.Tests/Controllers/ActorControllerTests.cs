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
    public class ActorControllerTests
    {
        private readonly Mock<IActorService> _mockActorService = new Mock<IActorService>();
        private readonly ActorController _actorController;

        public ActorControllerTests()
        {
            _actorController = new ActorController(_mockActorService.Object);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode200_WhenActorsExist()
        {
            // Arrange
            List<ActorResponse> actors = new List<ActorResponse>();

            actors.AddRange(ActorResponseList());

            _mockActorService.Setup(x => x.GetAllActorsAsync())
                .ReturnsAsync(actors);

            // Act
            var result = await _actorController.GetAll();
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode204_WhenNoActorsExist()
        {
            // Arrange
            List<ActorResponse> actors = new List<ActorResponse>();

            _mockActorService.Setup(x => x.GetAllActorsAsync())
                .ReturnsAsync(actors);

            // Act
            var result = await _actorController.GetAll();
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(204, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode500_WhenServiceReturnsNull()
        {
            // Arrange
            List<ActorResponse> games = new List<ActorResponse>();

            _mockActorService
                .Setup(x => x.GetAllActorsAsync())
                .ReturnsAsync(() => null);

            // Act
            var result = await _actorController.GetAll();
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            _mockActorService
                .Setup(x => x.GetAllActorsAsync())
                .ReturnsAsync(() => throw new Exception("test"));

            // Act
            var result = await _actorController.GetAll();
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetById_ShouldReturnStatusCode200_WhenActorExists()
        {
            // Arrange
            int actorId = 1;

            _mockActorService
                .Setup(x => x.GetActorByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(ActorResponse());

            // Act
            var result = await _actorController.GetById(actorId);
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetById_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            int actorId = 1;

            _mockActorService
                .Setup(x => x.GetActorByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(() => throw new Exception("Test"));

            // Act
            var result = await _actorController.GetById(actorId);
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetById_ShouldReturnStatusCode404_WhenActorDoesNotExist()
        {
            // Arrange
            int actorId = 1;

            _mockActorService
                .Setup(x => x.GetActorByIdAsync(actorId))
                .ReturnsAsync(() => null);

            // Act
            var result = await _actorController.GetById(actorId);
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(404, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Create_ShouldReturnStatusCode200_WhenActorIsCreated()
        {
            // Arrange
            _mockActorService
                .Setup(x => x.CreateActorAsync(It.IsAny<ActorRequest>()))
                .ReturnsAsync(ActorResponse());

            // Act
            var result = await _actorController.Create(ActorRequest());
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Create_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            _mockActorService
                .Setup(x => x.CreateActorAsync(It.IsAny<ActorRequest>()))
                .ReturnsAsync(() => throw new Exception("Test"));

            // Act
            var result = await _actorController.Create(ActorRequest());
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Update_ShouldReturnStatusCode200_WhenUpdateIsSuccessful()
        {
            // Arrange
            int actorId = 1;

            _mockActorService
                .Setup(x => x.UpdateActorByIdAsync(It.IsAny<int>(), It.IsAny<ActorRequest>()))
                .ReturnsAsync(ActorResponse());

            // Act
            var result = await _actorController.Update(actorId, ActorRequest());
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Update_ShouldReturnStatusCode404_WhenActorToUpdateIsNotFound()
        {
            // Arrange
            int actorId = 1;

            _mockActorService
                .Setup(x => x.UpdateActorByIdAsync(It.IsAny<int>(), It.IsAny<ActorRequest>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _actorController.Update(actorId, ActorRequest());
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(404, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Update_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            int actorId = 1;

            _mockActorService
                .Setup(x => x.UpdateActorByIdAsync(It.IsAny<int>(), It.IsAny<ActorRequest>()))
                .ReturnsAsync(() => throw new Exception("Test"));

            // Act
            var result = await _actorController.Update(actorId, ActorRequest());
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Delete_ShouldReturnStatusCode200_WhenDeleteIsSuccessful()
        {
            // Arrange
            int actorId = 1;

            _mockActorService
                .Setup(x => x.DeleteActorByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(ActorResponse());

            // Act
            var result = await _actorController.Delete(actorId);
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Delete_ShouldReturnStatusCode404_WhenActorToDeleteIsNotFound()
        {
            // Arrange
            int actorId = 1;

            _mockActorService
                .Setup(x => x.DeleteActorByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _actorController.Delete(actorId);
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(404, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Delete_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            int actorId = 1;

            _mockActorService
                .Setup(x => x.DeleteActorByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(() => throw new Exception("Test"));

            // Act
            var result = await _actorController.Delete(actorId);
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        private IEnumerable<ActorResponse> ActorResponseList()
        {
            return new List<ActorResponse>()
            {
                new ActorResponse()
                {
                    Id = 1,
                    Name = "Test Name",
                    ImdbLink = "imdblink.dk",
                    Movies = new List<ActorResponseMovie>()
                },
                new ActorResponse()
                {
                    Id = 2,
                    Name = "Test Name2",
                    ImdbLink = "imdblink2.dk",
                    Movies = new List<ActorResponseMovie>()
                }
            };
        }

        private ActorResponse ActorResponse()
        {
            return new ActorResponse()
            {
                Id = 1,
                Name = "Test Name",
                ImdbLink = "imdblink.dk",
                Movies = new List<ActorResponseMovie>()
            };
        }

        private ActorRequest ActorRequest()
        {
            return new ActorRequest()
            {
                Name = "Test Name",
                ImdbLink = "imdblink.dk",
            };
        }
    }
}
