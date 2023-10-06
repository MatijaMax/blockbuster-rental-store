using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using MovieStoreApi;
using MovieStoreCore.Domain;
using MovieStoreInfrastructure;
using MovieStoreInfrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Configuration
builder.Configuration.AddJsonFile("appsettings.json");
var configuration = builder.Configuration;

//Configure CORS middleware
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost4200", builder =>
    {
        builder.WithOrigins("http://localhost:4200")
               .AllowAnyHeader()
               .AllowAnyMethod()
               .AllowCredentials();
    });
});

// Configure JWT authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration, "AzureAd");

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

//Build the app
var app = builder.Build();
app.UseCors("AllowLocalhost4200");

// Automate Update-Database command
using var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetRequiredService<MovieStoreContext>();
dbContext.Database.Migrate();

// Configure OpenApi/Swagger
app.UseOpenApi(); // serve OpenAPI/Swagger documents
app.UseSwaggerUi3(); // serve Swagger 
app.UseAuthentication(); // Enable JWT authentication
app.UseAuthorization();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();
app.MapControllers();
app.Run();
