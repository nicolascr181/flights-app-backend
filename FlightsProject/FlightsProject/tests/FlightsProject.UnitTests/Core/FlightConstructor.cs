
using FlightsProject.Core.Entities;
namespace FlightsProject.UnitTests.Core;
using Xunit;

public class FlightConstructor
{
  private readonly string _testOrigin = "MZL";
  private Flight? _testFlight;

  private Flight CreateFlight()
  {
    return new Flight("MZL", "BOG", 2312.2, new Transport("AV", "1010"));
  }

  [Fact]
  public void InitializesName()
  {
    _testFlight = CreateFlight();

    Assert.Equal(_testOrigin, _testFlight.Origin);
  }
}


