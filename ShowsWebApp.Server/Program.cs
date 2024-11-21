using ShowsWebApp.Server.Mappers;
using Microsoft.AspNetCore.Builder;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection; // Add this line
using Microsoft.EntityFrameworkCore;
using ShowsWebApp.Server.Data;
using ShowsWebApp.Server.Repositories.ShowRepos;
using ShowsWebApp.Server.Repositories;
using ShowsWebApp.Server.Services;

var builder = WebApplication.CreateBuilder(args);

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Add DbContext
builder.Services.AddDbContext<ShowsWebAppServerContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ShowsWebAppServerContext") ?? throw new InvalidOperationException("Connection string 'ShowsWebAppServerContext' not found.")));


// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IShowRepository, ShowRepository>();

// Add services
builder.Services.AddScoped(typeof(IService<,>), typeof(Service<,>));
builder.Services.AddScoped<IShowService, ShowService>();
builder.Services.AddScoped<ISeasonService, SeasonService>();
builder.Services.AddScoped<IEpisodeService, EpisodeService>();



var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
