using CinemaNVS.DAL.Database.Entities.Movies;
using CinemaNVS.DAL.Repositories.Movies;
using CinemasNVS.BLL.DTOs;
using CinemasNVS.BLL.Services.MovieServices;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace CinemaNVS.Tests.Services
{
    public class DirectorServiceTests
    {
        private readonly Mock<IDirectorRepository> _directorRepositoryMock = new Mock<IDirectorRepository>();
        private readonly IDirectorService _directorService;

        public DirectorServiceTests()
        {
            _directorService = new DirectorService(_directorRepositoryMock.Object);
        }

        [Fact]
        public async void GetAllDirectorsAsync_ShouldReturnListOfDirectorResponses_WhenDirectorsExist()
        {
            //Arrange
            _directorRepositoryMock
                .Setup(x => x.SelectAllDirectorsAsync())
                .ReturnsAsync(DirectorList());

            //Act
            var result = (List<DirectorResponse>)await _directorService.GetAllDirectorsAsync();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<List<DirectorResponse>>(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async void GetAllDirectorsAsync_ShouldReturnEmptyListOfDirectorResponses_WhenNoDirectorsExist()
        {
            //Arrange
            IEnumerable<Director> emptyDirectorList = new List<Director>();

            _directorRepositoryMock
                .Setup(x => x.SelectAllDirectorsAsync())
                .ReturnsAsync(emptyDirectorList);

            //Act
            var result = (List<DirectorResponse>)await _directorService.GetAllDirectorsAsync();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<List<DirectorResponse>>(result);
            Assert.Empty(result);
        }

        [Fact]
        public async void GetDirectorByIdAsync_ShouldReturnDirectorResponse_WhenDirectorExists()
        {
            //Arrange
            int directorId = 1;

            _directorRepositoryMock
                .Setup(x => x.SelectDirectorByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(Director());

            //Act
            var result = await _directorService.GetDirectorByIdAsync(directorId);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<DirectorResponse>(result);
            Assert.Equal(directorId, result.Id);
        }

        [Fact]
        public async void GetDirectorByIdAsync_ShouldReturnNull_WhenDirectorDoesNotExist()
        {
            //Arrange
            int directorId = 1;

            _directorRepositoryMock
                .Setup(x => x.SelectDirectorByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            //Act
            var result = await _directorService.GetDirectorByIdAsync(directorId);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async void UpdateDirectorByIdAsync_ShouldReturnDirectorResponse_WhenDirectorIsSuccessfullyUpdated()
        {
            //Arrange
            int directorId = 1;

            _directorRepositoryMock
                .Setup(x => x.UpdateDirectorByIdAsync(It.IsAny<Director>(), It.IsAny<int>()))
                .ReturnsAsync(Director());

            //Act
            var result = await _directorService.UpdateDirectorByIdAsync(directorId, DirectorRequest());

            //Assert
            Assert.NotNull(result);
            Assert.IsType<DirectorResponse>(result);
            Assert.Equal(directorId, result.Id);
        }

        [Fact]
        public async void UpdateDirectorByIdAsync_ShouldReturnNull_WhenDirectorIsNotUpdated()
        {
            //Arrange
            int directorId = 1;

            _directorRepositoryMock
                .Setup(x => x.UpdateDirectorByIdAsync(It.IsAny<Director>(), It.IsAny<int>()))
                .ReturnsAsync(() => null);

            //Act
            var result = await _directorService.UpdateDirectorByIdAsync(directorId, DirectorRequest());

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async void CreateDirectorAsync_ShouldReturnDirectorResponse_WhenDirectorIsSuccessfullyCreated()
        {
            //Arrange
            _directorRepositoryMock
                .Setup(x => x.InsertDirectorAsync(It.IsAny<Director>()))
                .ReturnsAsync(Director());

            //Act
            var result = await _directorService.CreateDirectorAsync(DirectorRequest());

            //Assert
            Assert.NotNull(result);
            Assert.IsType<DirectorResponse>(result);
            Assert.Equal("Test Name", result.Name);
        }

        [Fact]
        public async void CreateDirectorAsync_ShouldReturnNull_WhenDirectorIsNotCreated()
        {
            //Arrange
            _directorRepositoryMock
                .Setup(x => x.InsertDirectorAsync(It.IsAny<Director>()))
                .ReturnsAsync(() => null);

            //Act
            var result = await _directorService.CreateDirectorAsync(DirectorRequest());

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async void DeleteDirectorById_ShouldReturnDirectorResponse_WhenDirectorIsSuccessfullyDeleted()
        {
            //Arrange
            int directorId = 1;

            _directorRepositoryMock
                .Setup(x => x.DeleteDirectorByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(Director());

            //Act
            var result = await _directorService.DeleteDirectorByIdAsync(directorId);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<DirectorResponse>(result);
            Assert.Equal(directorId, result.Id);
        }


        [Fact]
        public async void DeleteDirectorById_ShouldReturnNull_WhenDirectorIsNotDeleted()
        {
            //Arrange
            int directorId = 1;

            _directorRepositoryMock
                .Setup(x => x.DeleteDirectorByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            //Act
            var result = await _directorService.DeleteDirectorByIdAsync(directorId);

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

        private DirectorRequest DirectorRequest()
        {
            return new DirectorRequest()
            {
                Name = "Test Name",
                ImdbLink = "imdblink.dk"
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
