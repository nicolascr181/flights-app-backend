using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace FlightsProject.Web.Controllers;

/// <summary>
/// Controller for API Errors
/// </summary>
public class ErrorsController : ControllerBase
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("/error")]
    public IActionResult Error()
    {
      Exception? exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

      return Problem();
    }
  
}
