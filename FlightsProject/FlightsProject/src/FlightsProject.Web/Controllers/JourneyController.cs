﻿using FlightsProject.UseCases.Journeys.List;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlightsProject.Web.Controllers;

[Route("v1/journey")]
[ApiController]
public class JourneyController : APIController
{
  private readonly ISender _mediator;

  public JourneyController(ISender mediator)
  {
    _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
  }


  /// <summary>
  /// Retrieves a list of journeys filtered by origin and destination.
  /// </summary>
  /// <param name="command">Filter criteria for journeys.</param>
  /// <returns>A list of matching journeys or an error response.</returns>
  /// <response code="200">Returns the list of journeys.</response>
  /// <response code="400">If the request is invalid.</response>
  /// <response code="500">If an internal error occurs.</response>
  [HttpPost]
  [ProducesResponseType(StatusCodes.Status200OK)] // Successful response
  [ProducesResponseType(StatusCodes.Status400BadRequest)] // Bad request
  [ProducesResponseType(StatusCodes.Status500InternalServerError)] // Internal server error
  [Produces("application/json")]
  public async Task<IActionResult> ListJourneys([FromBody] FilterJourneyCommand command)
  {
    var journeysResult = await _mediator.Send(command);

    return journeysResult.Match(
        journey => Ok(journey),
        errors => Problem(errors)
    );
  }
}
