using HotelHub.Application.Abstractions;
using HotelHub.Application.Dtos;

namespace HotelHub.Application.Commands.Login;

public sealed record LoginCommand(
    string Email,
    string Password,
    Guid? HotelId,
    bool RememberMe,
    string? IpAddress
) : ICommand<LoginDto>;