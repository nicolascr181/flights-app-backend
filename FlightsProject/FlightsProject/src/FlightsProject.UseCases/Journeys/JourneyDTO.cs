using FlightsProject.Core.Entities;

namespace FlightsProject.UseCases.Journeys;
public class JourneyDTO {

  public string? Origin { get; set; }
  public string? Destination { get; set; }

  public List<Journey>? Journeys { get; set; }
  public JourneyDTO()
  {

  }
}
