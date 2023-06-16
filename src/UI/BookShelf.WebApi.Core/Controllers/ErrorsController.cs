using BookShelf.Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace BookShelf.WebApi.Core.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
[ApiController]
public class ErrorsController : ControllerBase
{
    [Route("/error")]
    public IActionResult Error()
    {
        var exception = HttpContext.Features
            .Get<IExceptionHandlerFeature>()
            ?.Error;

        return exception switch
        {
            EntityNotFoundException => NotFound(),
            _ => Problem(title: exception?.Message)
        };
    }
}