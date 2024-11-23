using FlightsProject.Core.Entities;
using FlightsProject.Core.Interfaces;


namespace FlightsProject.UseCases.Flights.List;


internal sealed class ListFlightsQueryHandler : IRequestHandler<ListFlightsQuery, ErrorOr<IReadOnlyList<FlightDTO>>>
{
  private readonly IFlightRepository _flightRepository;

  public ListFlightsQueryHandler(IFlightRepository flightRepository)
  {
    _flightRepository = flightRepository ?? throw new ArgumentNullException(nameof(flightRepository)); ;
  }

  public async Task<ErrorOr<IReadOnlyList<FlightDTO>>> Handle(ListFlightsQuery request, CancellationToken cancellationToken)
  {
    try
    {
      IReadOnlyList<Flight> flights = await _flightRepository.GetFlightsAsync();

      return flights.Select(flight => new FlightDTO(
              flight.Origin,
              flight.Destination,
              flight.Price,
              flight.Transport
          )).ToList();
    }
    catch (Exception ex)
    {

      return Error.Failure("List Flight Failure ", ex.Message);

    }
  }
}
