using HotelHub.Application.Commands.CreateUser;
using HotelHub.Application.Queries.GetUserById;
using Microsoft.AspNetCore.Mvc;

namespace HotelHub.Api.Controllers;

public class UserController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateUserCommand command,
        CancellationToken cancellationToken)
    {
        var result = await Mediator.SendAsync(command, cancellationToken);
        return HandleResult(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var result = await Mediator.SendAsync(new GetUserByIdQuery(id), cancellationToken);
        return HandleResult(result);
    }
}
