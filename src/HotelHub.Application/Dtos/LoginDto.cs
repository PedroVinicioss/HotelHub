namespace HotelHub.Application.Dtos;

public sealed record LoginDto(
    string Username,
    string AccessToken,
    string? RefreshToken,
    List<Guid> HotelIds
);