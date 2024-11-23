using FlightsProject.Core.Interfaces;
using FlightsProject.Core.Primitives;
using FlightsProject.Infrastructure.Persistence;
using FlightsProject.Infrastructure.Persistence.Repositories;
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
    return services;
  }

  private static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
  {

    services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(configuration.GetConnectionString("SqliteConnection")));

    services.AddScoped<IApplicationDbContext>(sp =>
            sp.GetRequiredService<ApplicationDbContext>());

    services.AddScoped<IUnitOfWork>(sp =>
            sp.GetRequiredService<ApplicationDbContext>());

    services.AddScoped<IJourneyRepository, JourneyRepository>();
    services.AddScoped<IFlightRepository, FlightRepository>();

    return services;
  }
}
