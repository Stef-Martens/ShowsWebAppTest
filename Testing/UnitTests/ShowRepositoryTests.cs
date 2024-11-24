using Microsoft.EntityFrameworkCore;
using ShowsWebApp.Server.Data;
using ShowsWebApp.Server.Models;
using ShowsWebApp.Server.Repositories.ShowRepos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;


namespace Testing.UnitTest
{
    public class ShowRepositoryTests
    {
        private readonly ShowsWebAppServerContext _context;
        private readonly ShowRepository _repository;

        public ShowRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ShowsWebAppServerContext>()
                .UseInMemoryDatabase("TestDatabase")
                .Options;

            _context = new ShowsWebAppServerContext(options);
            _repository = new ShowRepository(_context);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllShows()
        {
            // Arrange
            var show1 = new Show { Title = "Show 1", Genre = "Drama", Description = "Description 1", Language = "English" };
            var show2 = new Show { Title = "Show 2", Genre = "Comedy", Description = "Description 2", Language = "English" };

            _context.Shows.AddRange(show1, show2);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetAllAsync();

            // Assert
            result.Should().Contain(new[] { show1, show2 });
        }
    }
}
