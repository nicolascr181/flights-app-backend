using FlightsProject.Core.Entities;
using FlightsProject.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;


namespace FlightsProject.Infrastructure.Data;
public static class SeedData
{
 
  public static void Initialize(IServiceProvider serviceProvider)
  {
    using (var dbContext = new ApplicationDbContext(
        serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>(),null))
    {

      if (dbContext.Flights.Any()) return;

      PopulateTestData(dbContext);

    }
  }
  public static void PopulateTestData(ApplicationDbContext dbContext)
  {
    string jsonString = File.ReadAllText("markets.json");

    List<Flight>? flights = JsonConvert.DeserializeObject<List<Flight>>(jsonString);

    if(flights is not null)
    {
      foreach (Flight flight in flights)
      {
        dbContext.Add(flight);
      }
      dbContext.SaveChanges();
    }

  }
}
