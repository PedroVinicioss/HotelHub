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

        return MapError(result.Error);
    }

    protected IActionResult HandleResult(Result result)
    {
        if (result.IsSuccess)
            return NoContent();

        return MapError(result.Error);
    }

    private IActionResult MapError(Error error) => error.Type switch
    {
        ErrorType.Validation   => BadRequest(error),
        ErrorType.NotFound     => NotFound(error),
        ErrorType.Unauthorized => Unauthorized(error),
        ErrorType.Conflict     => Conflict(error),
        _                      => StatusCode(500, error)
    };
}
