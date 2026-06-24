using System.Security.Claims;
using HotelHub.Domain.Entities;

namespace HotelHub.Application.Abstractions;

public interface ITokenService
{
    string GenerateAccessToken(User user, IEnumerable<Guid> hotelIds);
    string GenerateRefreshToken();
    ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
}
