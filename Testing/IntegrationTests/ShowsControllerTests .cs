using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using ShowsWebApp.Server.Data;
using ShowsWebApp.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;


namespace Testing.IntegrationTests
{
    public class ShowsControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public ShowsControllerTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    // Replace DbContext with InMemory database for testing
                    var descriptor = services.SingleOrDefault(
                        d => d.ServiceType == typeof(DbContextOptions<ShowsWebAppServerContext>));

                    services.Remove(descriptor);

                    services.AddDbContext<ShowsWebAppServerContext>(options =>
                        options.UseInMemoryDatabase("TestDatabase"));
                });
            }).CreateClient();
        }

        [Fact]
        public async Task GetShowDTO_ShouldReturnOkWithShows()
        {
            var response = await _client.GetAsync("/api/Shows");
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }
    }

}
