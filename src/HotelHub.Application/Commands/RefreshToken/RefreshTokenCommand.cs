using HotelHub.Application.Abstractions;
using HotelHub.Application.Dtos;

namespace HotelHub.Application.Commands.RefreshToken;

public sealed record RefreshTokenCommand(
    string AccessToken,
    string RefreshToken,
    string? IpAddress
) : ICommand<LoginDto>;
