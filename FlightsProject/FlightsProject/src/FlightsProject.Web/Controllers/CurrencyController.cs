using FlightsProject.UseCases.Conversion.List;
using FlightsProject.UseCases.Journeys.List;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlightsProject.Web.Controllers;

[Route("v1/currency")]
[ApiController]
public class CurrencyController: APIController
{
  private readonly ISender _mediator;

  public CurrencyController(ISender mediator)
  {
    _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
  }

  /// <summary>
  /// Retrieves a list of all currencies
  /// </summary>
  /// <returns>A list of all currencies or an error response.</returns>
  /// <response code="200">Returns the list of journeys.</response>
  /// <response code="400">If the request is invalid.</response>
  /// <response code="500">If an internal error occurs.</response>
  [HttpGet]
  public async Task<IActionResult> GetCurrencyDetails()
  {
    var result = await _mediator.Send(new GetCurrenciesQuery());

    return result.Match(
        details => Ok(details), // Returns 200 OK with the detailed information
        errors => Problem(errors) // If error occurs, returns the error response
    );
  }
}
