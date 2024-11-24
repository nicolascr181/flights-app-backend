using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightsProject.Core.Entities;

namespace FlightsProject.Core.Interfaces;
public interface ICurrencyConverter
{
  /// <summary>
  /// Converts an amount from one currency to another.
  /// </summary>
  /// <param name="amount">The amount to convert.</param>
  /// <param name="targetCurrency">The currency to convert the amount to.</param>
  /// <param name="baseCurrency">The currency from which to convert. Defaults to "USD".</param>
  /// <returns>The converted amount.</returns>
  Task<double> Convert(double amount, string targetCurrency, string baseCurrency = "USD");

  /// <summary>
  /// Retrieves all the available currency details from the external API.
  /// </summary>
  /// <returns>A dictionary containing the details of all supported currencies.</returns>
  Task<IDictionary<string, CurrencyDetails>> GetCurrencyDetails();
}
