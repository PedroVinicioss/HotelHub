namespace HotelHub.Application.Dtos;

public sealed record UserDto(
    Guid Id,
    string Name,
    string Email,
    string Role
);
