using FlightsProject.Core.Primitives;

namespace FlightsProject.Core.Entities;
public sealed class Journey: AggregateRoot
{
  public Guid Id { get; private set; }
  public string? Origin { get; set; }
  public string? Destination { get; set; }
  public double TotalPrice { get; set; }
  public string Currency { get; set; } = "en-US";
  public List<Flight>? Flights { get; set; }

  public Journey(string origin, string destination, List<Flight> flights, string currency)
  {
    Origin = origin;
    Destination = destination;
    TotalPrice = 0;
    Flights = flights;
    Currency = currency;
  }

  public Journey() {
    TotalPrice = 0;
  }

  public double TotalFlightsPrice => Math.Round(Flights?.Sum(flight => flight?.Price ?? 0) ?? 0, 2);
}



