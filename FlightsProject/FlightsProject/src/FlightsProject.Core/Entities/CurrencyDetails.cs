using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightsProject.Core.Entities;
public class CurrencyDetails
{
  public string Symbol { get; set; }
  public string Name { get; set; }
  public string Code { get; set; }
  public int DecimalDigits { get; set; }
  public int Rounding { get; set; }

  public CurrencyDetails(string symbol,string name,string code,int decimalDigits,int rounding)
  {
    Symbol = symbol;
    Name = name;
    Code = code;
    DecimalDigits = decimalDigits;
    Rounding = rounding;
  }
}
