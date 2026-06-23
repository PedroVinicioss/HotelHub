using HotelHub.Application.Abstractions;
using HotelHub.Domain.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace HotelHub.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class BaseController : ControllerBase
{
    private IMediator? _mediator;

    protected IMediator Mediator =>
        _mediator ??= HttpContext.RequestServices.GetRequiredService<IMediator>();

    protected IActionResult HandleResult<T>(Result<T> result)
    {
        if (result.IsSuccess)
            return Ok(result.Value);

        return result.Error.Code.StartsWith("Validation")
            ? BadRequest(result.Error)
            : result.Error.Code.EndsWith("NotFound")
                ? NotFound(result.Error)
                : Conflict(result.Error);
    }

    protected IActionResult HandleResult(Result result)
    {
        if (result.IsSuccess)
            return NoContent();

        return result.Error.Code.StartsWith("Validation")
            ? BadRequest(result.Error)
            : result.Error.Code.EndsWith("NotFound")
                ? NotFound(result.Error)
                : Conflict(result.Error);
    }
}
