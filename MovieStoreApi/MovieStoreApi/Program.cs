using Microsoft.EntityFrameworkCore;
using MovieStoreApi;
using MovieStoreCore.Domain;
using MovieStoreInfrastructure;
using MovieStoreInfrastructure.Repositories;

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

// Configure MediatR
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

// Initialize repositories
builder.Services.AddTransient<IRepository<Movie>, GenericRepository<Movie>>();
builder.Services.AddTransient<IRepository<Customer>, GenericRepository<Customer>>();
builder.Services.AddTransient<IRepository<PurchasedMovie>, GenericRepository<PurchasedMovie>>();

//Schema name generation
builder.Services.AddOpenApiDocument(cfg =>
{
    cfg.SchemaNameGenerator = new CustomSwaggerSchemaNameGenerator();
});

// Add services to the container.

builder.Services.AddControllers();

var app = builder.Build();

// Configure OpenApi/Swagger

app.UseOpenApi(); // serve OpenAPI/Swagger documents
app.UseSwaggerUi3(); // serve Swagger 



// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
