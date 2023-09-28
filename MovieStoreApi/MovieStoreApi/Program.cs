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
    options.UseSqlServer(configuration.GetConnectionString("MovieStoreConnection"));

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
// Automate Update-Database command
using var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetRequiredService<MovieStoreContext>();
dbContext.Database.Migrate();


// Configure OpenApi/Swagger

app.UseOpenApi(); // serve OpenAPI/Swagger documents
app.UseSwaggerUi3(); // serve Swagger 

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
