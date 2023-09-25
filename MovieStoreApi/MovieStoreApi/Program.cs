using Microsoft.EntityFrameworkCore;
using MovieStoreInfrastructure;

var builder = WebApplication.CreateBuilder(args);

// Configuration
builder.Configuration.AddJsonFile("appsettings.json");
var configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddDbContext<MovieStoreContext>(options =>
{
    var connectionString = configuration.GetConnectionString("MovieStoreConnection");
    options.UseSqlServer(connectionString);
});

// Add services to the container.

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
