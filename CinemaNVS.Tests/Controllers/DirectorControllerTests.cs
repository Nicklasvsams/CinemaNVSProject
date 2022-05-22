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
    public class DirectorControllerTests
    {
        private readonly Mock<IDirectorService> _mockDirectorService = new Mock<IDirectorService>();
        private readonly DirectorController _directorController;

        public DirectorControllerTests()
        {
            _directorController = new DirectorController(_mockDirectorService.Object);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode200_WhenDirectorsExist()
        {
            // Arrange
            List<DirectorResponse> directors = new List<DirectorResponse>();

            directors.AddRange(DirectorResponseList());

            _mockDirectorService.Setup(x => x.GetAllDirectorsAsync())
                .ReturnsAsync(directors);

            // Act
            var result = await _directorController.GetAll();
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode204_WhenNoDirectorsExist()
        {
            // Arrange
            List<DirectorResponse> directors = new List<DirectorResponse>();

            _mockDirectorService.Setup(x => x.GetAllDirectorsAsync())
                .ReturnsAsync(directors);

            // Act
            var result = await _directorController.GetAll();
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(204, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode500_WhenServiceReturnsNull()
        {
            // Arrange
            List<DirectorResponse> games = new List<DirectorResponse>();

            _mockDirectorService
                .Setup(x => x.GetAllDirectorsAsync())
                .ReturnsAsync(() => null);

            // Act
            var result = await _directorController.GetAll();
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            _mockDirectorService
                .Setup(x => x.GetAllDirectorsAsync())
                .ReturnsAsync(() => throw new Exception("test"));

            // Act
            var result = await _directorController.GetAll();
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetById_ShouldReturnStatusCode200_WhenDirectorExists()
        {
            // Arrange
            int directorId = 1;

            _mockDirectorService
                .Setup(x => x.GetDirectorByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(DirectorResponse());

            // Act
            var result = await _directorController.GetById(directorId);
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetById_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            int directorId = 1;

            _mockDirectorService
                .Setup(x => x.GetDirectorByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(() => throw new Exception("Test"));

            // Act
            var result = await _directorController.GetById(directorId);
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetById_ShouldReturnStatusCode404_WhenDirectorDoesNotExist()
        {
            // Arrange
            int directorId = 1;

            _mockDirectorService
                .Setup(x => x.GetDirectorByIdAsync(directorId))
                .ReturnsAsync(() => null);

            // Act
            var result = await _directorController.GetById(directorId);
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(404, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Create_ShouldReturnStatusCode200_WhenDirectorIsCreated()
        {
            // Arrange
            _mockDirectorService
                .Setup(x => x.CreateDirectorAsync(It.IsAny<DirectorRequest>()))
                .ReturnsAsync(DirectorResponse());

            // Act
            var result = await _directorController.Create(DirectorRequest());
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Create_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            _mockDirectorService
                .Setup(x => x.CreateDirectorAsync(It.IsAny<DirectorRequest>()))
                .ReturnsAsync(() => throw new Exception("Test"));

            // Act
            var result = await _directorController.Create(DirectorRequest());
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Update_ShouldReturnStatusCode200_WhenUpdateIsSuccessful()
        {
            // Arrange
            int directorId = 1;

            _mockDirectorService
                .Setup(x => x.UpdateDirectorByIdAsync(It.IsAny<int>(), It.IsAny<DirectorRequest>()))
                .ReturnsAsync(DirectorResponse());

            // Act
            var result = await _directorController.Update(directorId, DirectorRequest());
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Update_ShouldReturnStatusCode404_WhenDirectorToUpdateIsNotFound()
        {
            // Arrange
            int directorId = 1;

            _mockDirectorService
                .Setup(x => x.UpdateDirectorByIdAsync(It.IsAny<int>(), It.IsAny<DirectorRequest>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _directorController.Update(directorId, DirectorRequest());
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(404, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Update_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            int directorId = 1;

            _mockDirectorService
                .Setup(x => x.UpdateDirectorByIdAsync(It.IsAny<int>(), It.IsAny<DirectorRequest>()))
                .ReturnsAsync(() => throw new Exception("Test"));

            // Act
            var result = await _directorController.Update(directorId, DirectorRequest());
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Delete_ShouldReturnStatusCode200_WhenDeleteIsSuccessful()
        {
            // Arrange
            int directorId = 1;

            _mockDirectorService
                .Setup(x => x.DeleteDirectorByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(DirectorResponse());

            // Act
            var result = await _directorController.Delete(directorId);
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Delete_ShouldReturnStatusCode404_WhenDirectorToDeleteIsNotFound()
        {
            // Arrange
            int directorId = 1;

            _mockDirectorService
                .Setup(x => x.DeleteDirectorByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _directorController.Delete(directorId);
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(404, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Delete_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            int directorId = 1;

            _mockDirectorService
                .Setup(x => x.DeleteDirectorByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(() => throw new Exception("Test"));

            // Act
            var result = await _directorController.Delete(directorId);
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        private IEnumerable<DirectorResponse> DirectorResponseList()
        {
            return new List<DirectorResponse>()
            {
                new DirectorResponse()
                {
                    Id = 1,
                    Name = "Test Name",
                    ImdbLink = "imdblink.dk",
                    Movies = new List<DirectorResponseMovie>()
                },
                new DirectorResponse()
                {
                    Id = 2,
                    Name = "Test Name2",
                    ImdbLink = "imdblink2.dk",
                    Movies = new List<DirectorResponseMovie>()
                }
            };
        }

        private DirectorResponse DirectorResponse()
        {
            return new DirectorResponse()
            {
                Id = 1,
                Name = "Test Name",
                ImdbLink = "imdblink.dk",
                Movies = new List<DirectorResponseMovie>()
            };
        }

        private DirectorRequest DirectorRequest()
        {
            return new DirectorRequest()
            {
                Name = "Test Name",
                ImdbLink = "imdblink.dk",
            };
        }
    }
}
