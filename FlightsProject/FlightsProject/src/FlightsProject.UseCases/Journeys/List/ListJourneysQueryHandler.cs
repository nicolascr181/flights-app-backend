using FlightsProject.Core.Entities;
using FlightsProject.Core.Interfaces;
using FlightsProject.UseCases.Conversion;
using FlightsProject.UseCases.Graphs;

namespace FlightsProject.UseCases.Journeys.List;
public sealed class ListJourneysQueryHandler : IRequestHandler<FilterJourneyCommand, ErrorOr<List<JourneyDTO>>>
{
  private readonly IFlightRepository _flightRepository;
  private readonly IJourneyPriceConverter _journeyPriceConverter;

  public ListJourneysQueryHandler(IFlightRepository flightRepository, IJourneyPriceConverter journeyPriceConverter)
  {
    _flightRepository = flightRepository ?? throw new ArgumentNullException(nameof(flightRepository));
    _journeyPriceConverter = journeyPriceConverter ?? throw new ArgumentNullException(nameof(journeyPriceConverter));
  }
  public async Task<ErrorOr<List<JourneyDTO>>> Handle(FilterJourneyCommand command, CancellationToken cancellationToken)
  {
    try
    {
      List<JourneyDTO> journeysResponse = new List<JourneyDTO>();

      string origin = command.Origin;
      string destination = command.Destination;
      string currencyType = command.CurrencyType;

      IReadOnlyList<Flight> flights = await _flightRepository.GetFlightsAsync();
      List<Flight> flightList = flights.ToList();

      List<Journey> journeys = GenerateJourneys(flightList, origin, destination, command);


      // Convert prices using the service
      if (!string.IsNullOrEmpty(currencyType) && !currencyType.Equals("USD"))
      {
        journeys = await _journeyPriceConverter.ConvertPrices(journeys, currencyType);
      }

      journeysResponse.Add(new JourneyDTO
      {
        Origin = origin,
        Destination = destination,
        Journeys = journeys
      });

      if (command.TripType == "Roundtrip")
      {
        string returnOrigin = destination;
        string returnDestination = origin;

        List<Journey> returnJourneys = GenerateJourneys(flightList, returnOrigin, returnDestination,command);

        journeysResponse.Add(new JourneyDTO
        {
          Origin = returnOrigin,
          Destination = returnDestination,
          Journeys = returnJourneys
        });
      }

      return journeysResponse;
    }
    catch (Exception ex)
    {
      return Error.Failure("List Journey Failure", ex.Message);
    }
  }

  List<Journey> GenerateJourneys(List<Flight> flightList, string source, string destination, FilterJourneyCommand command)
  {
    var journeyFlights = Graph.FindAllPaths(flightList, source, destination);

    return journeyFlights.Select(flightsAux => new Journey
    {
      Origin = source,
      Destination = destination,
      Price = 0,
      Flights = flightsAux,
      Currency = command.CurrencyType
    }).ToList();
  }
}
