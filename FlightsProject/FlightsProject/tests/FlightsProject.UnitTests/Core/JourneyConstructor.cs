using FlightsProject.Core.Entities;
using Xunit;

namespace FlightsProject.UnitTests.Core;
public class JourneyConstructor
{
  private readonly double _testTotalPrice = 5511.60;
  private Journey? _testJourney;
  private Journey CreateJourney()
  {
    List<Flight> flights = [
      new Flight("MZL", "PEI", 2001.2, new Transport("AV","4212")),
      new Flight("MZL", "PEI", 1500.2, new Transport("AV","4212")),
      new Flight("MZL", "PEI", 2010.2, new Transport("AV","4212"))
      ];

    return new Journey("MZL","BOG",flights, "USD");
  }


  [Fact]
  public void ComputeTotalPrice()
  {
    _testJourney = CreateJourney();

    Assert.Equal(_testTotalPrice, _testJourney.TotalFlightsPrice);
  }
}
