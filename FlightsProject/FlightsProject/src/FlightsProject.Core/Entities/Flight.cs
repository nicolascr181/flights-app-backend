namespace FlightsProject.Core.Entities;
public sealed class Flight
{
  public Guid Id { get; private set; }
  public string? Origin { get; set; }
  public string? Destination { get; set; }
  public double? Price { get; set; }
  public Transport? Transport { get; set; }

  public Flight(string origin, string destination, double price, Transport transport)
  {
    Origin = origin;
    Destination = destination;
    Price = price;
    Transport = transport;
  }

  public Flight()
  {
    
  }
}
