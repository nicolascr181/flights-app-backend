using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightsProject.Core.Entities;

namespace FlightsProject.Core.Interfaces;
public interface IJourneyPriceConverter
{
  List<Journey> ConvertPrices(List<Journey> journeys, string targetCurrency);
}
