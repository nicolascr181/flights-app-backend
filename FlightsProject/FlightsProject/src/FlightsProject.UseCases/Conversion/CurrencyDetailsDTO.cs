using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightsProject.UseCases.Conversion;
public record CurrencyDetailsDTO(string? symbol, string? name, string? code, int? decimalDigits, int? rounding);
