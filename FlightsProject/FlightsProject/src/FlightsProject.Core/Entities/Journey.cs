using System.Globalization;
using FlightsProject.Core.Primitives;

namespace FlightsProject.Core.Entities;

public sealed class Journey: AggregateRoot
{
  public Guid Id { get; private set; }
  public string? Origin { get; set; }
  public string? Destination { get; set; }
  public double? Price { get; set; }

  public string Currency { get; set; } = "en-US";

  public List<Flight>? Flights { get; set; }

  public Journey(string origin, string destination, double price, List<Flight> flights, string currency)
  {
    Origin = origin;
    Destination = destination;
    Price = price;
    Flights = flights;
    Currency = currency;
  }

  public Journey() { }

  public string TotalPrice => (Flights?.Sum(flight => flight?.Price ?? 0) ?? 0).ToString("C", CultureInfo.CreateSpecificCulture(Currency));
}



