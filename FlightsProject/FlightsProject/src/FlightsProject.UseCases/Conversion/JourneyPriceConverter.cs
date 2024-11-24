using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightsProject.Core.Entities;
using FlightsProject.Core.Interfaces;

namespace FlightsProject.UseCases.Conversion;
public class JourneyPriceConverter : IJourneyPriceConverter
{
  private readonly ICurrencyConverter _currencyConverter;

  public JourneyPriceConverter(ICurrencyConverter currencyConverter)
  {
    _currencyConverter = currencyConverter;
  }

  /// <summary>
  /// Convert prices for all flights only if exist
  /// </summary>
  /// <param name="journeys"></param>
  /// <param name="targetCurrency"></param>
  /// <returns></returns>
  public List<Journey> ConvertPrices(List<Journey> journeys, string targetCurrency)
  {
    foreach (Journey journey in journeys)
    {
      if (journey.Flights == null)
        continue;

      foreach (Flight flight in journey.Flights)
      {
        if (flight.Price.HasValue) // Check if Price is not null
        {
          flight.Price = _currencyConverter.Convert(flight.Price.Value, targetCurrency);
        }
      }
    }
    return journeys;
  }
}
