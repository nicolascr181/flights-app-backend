
namespace FlightsProject.UseCases.Flights.List;
public record ListFlightsQuery() : IRequest<ErrorOr<IReadOnlyList<FlightDTO>>>;
