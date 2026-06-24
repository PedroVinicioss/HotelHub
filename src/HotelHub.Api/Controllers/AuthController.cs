using HotelHub.Api.Security;
using HotelHub.Application.Commands.Login;
using HotelHub.Application.Commands.RefreshToken;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace HotelHub.Api.Controllers;

[Route("api/auth")]
public sealed class AuthController : BaseController
{
    [AllowAnonymous]
    [EnableRateLimiting(RateLimitPolicies.Auth)]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
    {
        var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();

        var command = new LoginCommand(
            request.Email,
            request.Password,
            request.HotelId,
            request.RememberMe,
            ipAddress);

        var result = await Mediator.SendAsync(command, cancellationToken);
        return HandleResult(result);
    }

    [AllowAnonymous]
    [EnableRateLimiting(RateLimitPolicies.Auth)]
    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest request, CancellationToken cancellationToken)
    {
        var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();

        var command = new RefreshTokenCommand(
            request.AccessToken,
            request.RefreshToken,
            ipAddress);

        var result = await Mediator.SendAsync(command, cancellationToken);
        return HandleResult(result);
    }
}

public sealed record LoginRequest(string Email, string Password, Guid? HotelId, bool RememberMe);
public sealed record RefreshTokenRequest(string AccessToken, string RefreshToken);
