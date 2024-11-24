using Moq;
using Xunit;
using ShowsWebApp.Server.Controllers;
using ShowsWebApp.Server.Services;
using ShowsWebApp.Server.Models;
using ShowsWebApp.Server.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using FluentAssertions;
using System.Threading.Tasks;



namespace Testing
{
    public class ShowsControllerTests
    {
        private readonly Mock<IShowService> _mockShowService;
        private readonly ShowsController _controller;

        public ShowsControllerTests()
        {
            _mockShowService = new Mock<IShowService>();
            _controller = new ShowsController(_mockShowService.Object);
        }

        [Fact]
        public async Task GetShows_ShouldReturnOkWithListOfShowDTOs()
        {
            // Arrange
            var mockShows = new List<ShowDTO>
            {
                new ShowDTO { Title = "Show 1", Genre = "Drama" },
                new ShowDTO {  Title = "Show 2", Genre = "Comedy" }
            };

            _mockShowService.Setup(service => service.GetAllAsync())
                .ReturnsAsync(mockShows);

            // Act
            var result = await _controller.GetShowDTO();

            // Assert
            var okResult = result.Result as OkObjectResult; // Extract OkObjectResult
            okResult.Should().NotBeNull();                 // Ensure it's not null
            okResult.Value.Should().BeEquivalentTo(mockShows); // Check the response content
        }
    }
}