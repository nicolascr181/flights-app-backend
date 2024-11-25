using ErrorOr;
using FlightsProject.Core.Entities;
using FlightsProject.Core.Interfaces;
using FlightsProject.UseCases.Conversion;
using FlightsProject.UseCases.Journeys.List;
using Moq;
using Xunit;

namespace FlightsProject.UnitTests.UseCases;

public class ListJourneysQueryHandlerTests
{
  private readonly Mock<IFlightRepository> _mockFlightRepository;

  private readonly Mock<IJourneyPriceConverter> _mockJourneyPriceConverter;

  private readonly ListJourneysQueryHandler _queryHandler;

  private readonly Mock<ICurrencyConverter> _mockCurrencyConverter;

  private readonly JourneyPriceConverter _priceConverter;

  public ListJourneysQueryHandlerTests()
  {
    _mockFlightRepository = new Mock<IFlightRepository>();
    _mockJourneyPriceConverter = new Mock<IJourneyPriceConverter>();
    _queryHandler = new ListJourneysQueryHandler(_mockFlightRepository.Object, _mockJourneyPriceConverter.Object);
    _mockCurrencyConverter = new Mock<ICurrencyConverter>();
    _priceConverter = new JourneyPriceConverter(_mockCurrencyConverter.Object);
  }

  [Fact]
  public async Task Handle_ReturnsExpectedJourneys()
  {
    // Arrange
    FilterJourneyCommand command = new FilterJourneyCommand
    ("MZL", "PEI", "USD", "Roundtrip");

    var flights = new List<Flight>
        {
            new Flight { Origin = "MZL", Destination = "BOG" },
            new Flight { Origin = "MZL", Destination = "PEI" },
            new Flight { Origin = "PEI", Destination = "BOG" },
            new Flight { Origin = "PEI", Destination = "MZL" },
            new Flight { Origin = "BOG", Destination = "BCN" },
            new Flight { Origin = "BOG", Destination = "JFK" },
            new Flight { Origin = "BOG", Destination = "MZL" },
            new Flight { Origin = "BOG", Destination = "PEI" },
            new Flight { Origin = "JFK", Destination = "BCN" },
            new Flight { Origin = "JFK", Destination = "MAD" },
            new Flight { Origin = "JFK", Destination = "BOG" },
            new Flight { Origin = "BCN", Destination = "BOG" },
            new Flight { Origin = "MAD", Destination = "BCN" }
        };

    _mockFlightRepository.Setup(repo => repo.GetFlightsAsync())
                        .ReturnsAsync(flights);


    // Act
    var result = await _queryHandler.Handle(command, default);

    //Assert
    Assert.NotNull(result.Value);
    Assert.Equal(2, result.Value.Count); // Roundtrip should have 2 journeys
    Assert.Equal("MZL", result.Value[0].Origin);
    Assert.Equal("PEI", result.Value[0].Destination);
  }

  [Fact]
  public async Task ConvertPrices_ShouldConvertJourneyPricesEUR()
  {
    // Arrange
    var flightsJourney1 = new List<Flight>
        {
            new Flight { Origin = "MZL", Destination = "BOG", Price=  100.0 },
            new Flight { Origin = "MZL", Destination = "PEI", Price=  150.0 },
            new Flight { Origin = "PEI", Destination = "BOG", Price=  0 },
        };
    var flightsJourney2 = new List<Flight>
        {
            new Flight { Origin = "MZL", Destination = "BOG", Price=  1000 }
        };

    var journeys = new List<Journey>
    {
        new Journey { Origin = "MZL", Destination = "PEI", Flights = flightsJourney1 },
        new Journey { Origin = "MZL", Destination = "PEI", Flights = flightsJourney2 }
    };


    string targetCurrency = "EUR";

    // Setup the mock to return a fixed conversion rate (e.g., 0.9 for EUR conversion)
    _mockCurrencyConverter
        .Setup(c => c.Convert(It.IsAny<double>(), It.Is<string>(s => s == targetCurrency), It.IsAny<string>()))  // Match all 3 parameters
        .ReturnsAsync((double amount, string target, string baseCurrency) => amount * 0.9);  // Handle all 3 parameters

    // Act
    var result = await _priceConverter.ConvertPrices(journeys, targetCurrency);

    // Assert
    Assert.NotNull(result);
    Assert.Equal(2, result.Count); // Ensure the number of journeys remains the same
    Assert.Equal(225, result[0].TotalPrice); // (100.0 + 150.0) * 0.9 = 225
    Assert.Equal(900, result[1].TotalPrice); // (1000 * 0.9) = 900
  }

  [Fact]
  public async Task ConvertPrices_ShouldConvertJourneyPricesUSD()
  {
    // Arrange
    var flightsJourney1 = new List<Flight>
        {
            new Flight { Origin = "MZL", Destination = "BOG", Price=  100.0 },
            new Flight { Origin = "MZL", Destination = "PEI", Price=  150.0 },
            new Flight { Origin = "PEI", Destination = "BOG", Price=  0 },
        };
    var flightsJourney2 = new List<Flight>
        {
            new Flight { Origin = "MZL", Destination = "BOG", Price=  1000 }
        };

    var journeys = new List<Journey>
    {
        new Journey { Origin = "MZL", Destination = "PEI", Flights = flightsJourney1 },
        new Journey { Origin = "MZL", Destination = "PEI", Flights = flightsJourney2 }
    };


    string targetCurrency = "USD";

    // Setup the mock to return a fixed conversion rate (e.g., 0.9 for EUR conversion)
    _mockCurrencyConverter
        .Setup(c => c.Convert(It.IsAny<double>(), It.Is<string>(s => s == targetCurrency), It.IsAny<string>()))  // Match all 3 parameters
        .ReturnsAsync((double amount, string target, string baseCurrency) => amount * 1);  // Handle all 3 parameters

    // Act
    var result = await _priceConverter.ConvertPrices(journeys, targetCurrency);

    // Assert
    Assert.NotNull(result);
    Assert.Equal(2, result.Count); // Ensure the number of journeys remains the same
    Assert.Equal(250, result[0].TotalPrice); // (100.0 + 150.0) = 250
    Assert.Equal(1000, result[1].TotalPrice); // (1000) = 1000
  }

}
