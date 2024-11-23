namespace FlightsProject.Core.Entities;
public sealed class Transport
{
  public Guid Id { get; private set; }
  public string FlightCarrier { get; set; }

  public string FlightNumber { get; set; }

  public Transport(string flightCarrier, string flightNumber)
  {
    FlightCarrier = flightCarrier;
    FlightNumber = flightNumber;
  }
}
