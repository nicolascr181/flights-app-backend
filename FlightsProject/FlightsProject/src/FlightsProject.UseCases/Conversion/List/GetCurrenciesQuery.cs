using FlightsProject.Core.Entities;

namespace FlightsProject.UseCases.Conversion.List;

public record GetCurrenciesQuery() : IRequest<ErrorOr<IDictionary<string, CurrencyDetails>>>;
