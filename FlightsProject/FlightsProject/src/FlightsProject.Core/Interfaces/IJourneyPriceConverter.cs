using FlightsProject.Core.Entities;

namespace FlightsProject.Core.Interfaces;

public interface IJourneyPriceConverter
{
  Task<List<Journey>> ConvertPrices(List<Journey> journeys, string targetCurrency);
}
