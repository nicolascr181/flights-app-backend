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
  public async Task<List<Journey>> ConvertPrices(List<Journey> journeys, string targetCurrency)
  {
    foreach (Journey journey in journeys)
    {
     
       journey.TotalPrice = await _currencyConverter.Convert(journey.TotalFlightsPrice, targetCurrency);
     
    }
    return journeys;
  }
}
