using System.ComponentModel.DataAnnotations;

namespace FlightsProject.UseCases.Journeys.List;
public record FilterJourneyCommand
(
  [Required]
  string Origin,
  [Required]
  string Destination,
  [Required]
  string CurrencyType,
  [Required]
  string TripType
) : IRequest<ErrorOr<List<JourneyDTO>>>;
