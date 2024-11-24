using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightsProject.Core.Entities;
using FlightsProject.Core.Interfaces;

namespace FlightsProject.UseCases.Conversion.List;
public sealed class GetCurrencyDetailsQueryHandler : IRequestHandler<GetCurrenciesQuery,ErrorOr<IDictionary<string, CurrencyDetails>>>
{
  private readonly ICurrencyConverter _currencyService;

  public GetCurrencyDetailsQueryHandler(ICurrencyConverter currencyService)
  {
    _currencyService = currencyService ?? throw new ArgumentNullException(nameof(currencyService));
  }

  public async Task<ErrorOr<IDictionary<string, CurrencyDetails>>> Handle(GetCurrenciesQuery request,CancellationToken cancellationToken)
  {
    try
    {

      var currencyDetails = await _currencyService.GetCurrencyDetails();

      return new Dictionary<string, CurrencyDetails>(currencyDetails);
    }
    catch (Exception ex)
    {
      return Error.Failure("List currencies Failure", ex.Message);
    }
  }

}
