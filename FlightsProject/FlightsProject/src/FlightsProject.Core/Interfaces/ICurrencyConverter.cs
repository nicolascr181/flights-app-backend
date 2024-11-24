using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightsProject.Core.Interfaces;
public interface ICurrencyConverter
{
  /// <summary>
  /// Allows to convert an amount currency to another currency
  /// </summary>
  /// <param name="amount"></param>
  /// <param name="targetCurrency"></param>
  /// <returns></returns>
  double Convert(double amount, string targetCurrency, string baseCurrency = "USD");
}
