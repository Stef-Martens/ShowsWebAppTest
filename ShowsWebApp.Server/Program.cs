using ShowsWebApp.Server.Mappers;
using Microsoft.AspNetCore.Builder;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection; // Add this line
using Microsoft.EntityFrameworkCore;
using ShowsWebApp.Server.Data;
using ShowsWebApp.Server.Repositories.ShowRepos;
using ShowsWebApp.Server.Repositories;
using ShowsWebApp.Server.Services;
using Microsoft.AspNetCore.Identity;
using ShowsWebApp.Server.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

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


// Configure Identity
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<ShowsWebAppServerContext>()
    .AddApiEndpoints();


// Add authentication and authorization
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();


var app = builder.Build();

app.UseCors("AllowAll");


app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    //app.ApplyMigrations();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.MapIdentityApi<User>();

app.Run();
