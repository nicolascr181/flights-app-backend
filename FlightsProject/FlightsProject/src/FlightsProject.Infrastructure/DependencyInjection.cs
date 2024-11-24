using FlightsProject.Core.Interfaces;
using FlightsProject.Core.Primitives;
using FlightsProject.Infrastructure.Persistence;
using FlightsProject.Infrastructure.Persistence.Repositories;
using FlightsProject.Infrastructure.Services;
using FlightsProject.UseCases.Conversion;
using FlightsProject.UseCases.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FlightsProject.Infrastructure;

public static class DependencyInjection
  {
  public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
  {
    services.AddPersistence(configuration);

    // Fetch the API key from appsettings.json
    string? apiKey = configuration["CurrencyApi:ApiKey"];

    // Validate that the API key is not null or empty
    if (string.IsNullOrEmpty(apiKey))
    {
      throw new InvalidOperationException("API key for CurrencyApi is missing in appsettings.json or environment variables.");
    }

    // Register the DynamicCurrencyConverter with the API key
    services.AddSingleton<ICurrencyConverter>(provider => new DynamicCurrencyConverter(apiKey));

    return services;
  }

  private static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
  {

    services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(configuration.GetConnectionString("SqliteConnection")));

    services.AddScoped<IApplicationDbContext>(sp =>sp.GetRequiredService<ApplicationDbContext>());

    services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

    services.AddScoped<IJourneyRepository, JourneyRepository>();
    services.AddScoped<IFlightRepository, FlightRepository>();
    services.AddScoped<IJourneyPriceConverter, JourneyPriceConverter>();
   
    return services;
  }
}
