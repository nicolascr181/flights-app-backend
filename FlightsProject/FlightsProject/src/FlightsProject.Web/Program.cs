
using System;
using FlightsProject.Infrastructure;
using FlightsProject.Infrastructure.Data;
using FlightsProject.Infrastructure.Persistence;
using FlightsProject.UseCases;
using FlightsProject.Web.Extensions;
using Serilog;
using Serilog.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

// Configure Logging
ConfigureLogging(builder);

// Add Services to the DI Container
ConfigureServices(builder);

// Build the Web Application
var app = builder.Build();

// Configure the HTTP Request Pipeline
ConfigureMiddleware(app);

// Seed the Database
SeedDatabase(app);

// Run the Application
app.Run();

// Logging Configuration
void ConfigureLogging(WebApplicationBuilder builder)
{
  Log.Logger = new LoggerConfiguration()
      .Enrich.FromLogContext()
      .WriteTo.Console()
      .CreateLogger();

  Log.Information("Starting web host");

  builder.Host.UseSerilog((_, config) => config.ReadFrom.Configuration(builder.Configuration));
}

// Service Registration
void ConfigureServices(WebApplicationBuilder builder)
{
  builder.Services.AddControllers();
  builder.Services.AddEndpointsApiExplorer();
  builder.Services.AddSwaggerGen();

  // Custom Application and Infrastructure Services
  builder.Services.AddInfrastructure(builder.Configuration);
  builder.Services.AddApplication();

  // CORS Policy
  builder.Services.AddCors(options =>
      options.AddPolicy("ApiCorsPolicy", policyBuilder =>
          policyBuilder.WithOrigins("http://localhost:4200")
                       .AllowAnyMethod()
                       .AllowAnyHeader()));
}

// Middleware and HTTP Request Pipeline Configuration
void ConfigureMiddleware(WebApplication app)
{
  if (app.Environment.IsDevelopment())
  {
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ApplyMigrations(); // Extension for applying migrations
  }

  app.UseHttpsRedirection();
  app.UseCors("ApiCorsPolicy");
  app.UseExceptionHandler("/error");

  app.MapControllers();
}

// Seed Database
void SeedDatabase(WebApplication app)
{
  using var scope = app.Services.CreateScope();
  var services = scope.ServiceProvider;

  try
  {
    var context = services.GetRequiredService<ApplicationDbContext>();
    context.Database.EnsureCreated(); // Ensure database creation
    SeedData.Initialize(services); // Custom seed data logic
  }
  catch (Exception ex)
  {
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred while seeding the database: {exceptionMessage}", ex.Message);
  }
}

// Public Program class for integration tests
public partial class Program
{
}
