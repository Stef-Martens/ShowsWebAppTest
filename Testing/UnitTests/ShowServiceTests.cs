using AutoMapper;
using Moq;
using ShowsWebApp.Server.DTOs;
using ShowsWebApp.Server.Models;
using ShowsWebApp.Server.Repositories.ShowRepos;
using ShowsWebApp.Server.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;

namespace Testing.UnitTest
{
    public class ShowServiceTests
    {
        private readonly Mock<IShowRepository> _mockShowRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly ShowService _service;

        public ShowServiceTests()
        {
            _mockShowRepository = new Mock<IShowRepository>();
            _mockMapper = new Mock<IMapper>();
            _service = new ShowService(_mockShowRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnMappedShowDTOs()
        {
            // Arrange
            var mockShows = new List<Show>
            {
                new Show { Title = "Show 1", Genre = "Drama" },
                new Show {  Title = "Show 2", Genre = "Comedy" }
            };

            var mockShowDTOs = new List<ShowDTO>
            {
                new ShowDTO {  Title = "Show 1", Genre = "Drama" },
                new ShowDTO {  Title = "Show 2", Genre = "Comedy" }
            };

            _mockShowRepository.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(mockShows);

            _mockMapper.Setup(mapper => mapper.Map<IEnumerable<ShowDTO>>(mockShows))
                .Returns(mockShowDTOs);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            result.Should().BeEquivalentTo(mockShowDTOs);

            _mockShowRepository.Verify(repo => repo.GetAllAsync(), Times.Once);
            _mockMapper.Verify(mapper => mapper.Map<IEnumerable<ShowDTO>>(mockShows), Times.Once);
        }
    }
}
