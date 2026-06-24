using HotelHub.Application.Abstractions;
using HotelHub.Application.Dtos;
using HotelHub.Application.Settings;
using HotelHub.Domain.Abstraction;
using HotelHub.Domain.Entities;
using RefreshTokenEntity = HotelHub.Domain.Entities.RefreshToken;
using HotelHub.Domain.Errors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace HotelHub.Application.Commands.RefreshToken;

public sealed class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, Result<LoginDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly ITokenService _tokenService;
    private readonly JwtSettings _jwtSettings;

    public RefreshTokenCommandHandler(
        IApplicationDbContext context,
        ITokenService tokenService,
        IOptions<JwtSettings> jwtSettings)
    {
        _context = context;
        _tokenService = tokenService;
        _jwtSettings = jwtSettings.Value;
    }

    public async Task<Result<LoginDto>> HandleAsync(RefreshTokenCommand request, CancellationToken cancellationToken = default)
    {
        var principal = _tokenService.GetPrincipalFromExpiredToken(request.AccessToken);

        if (principal is null)
            return AuthErrors.InvalidRefreshToken;

        var userIdClaim = principal.FindFirst("sub")?.Value;

        if (!Guid.TryParse(userIdClaim, out var userId))
            return AuthErrors.InvalidRefreshToken;

        var storedToken = await _context.RefreshTokens
            .Include(rt => rt.User)
                .ThenInclude(u => u.UserHotels)
            .FirstOrDefaultAsync(
                rt => rt.Token == request.RefreshToken && rt.UserId == userId,
                cancellationToken);

        if (storedToken is null || !storedToken.IsValid)
            return AuthErrors.InvalidRefreshToken;

        // Rotação: revoga o token atual e emite um novo
        storedToken.Revoke();

        var hotelIds = storedToken.User.UserHotels.Select(uh => uh.HotelId).ToList();
        var newAccessToken = _tokenService.GenerateAccessToken(storedToken.User, hotelIds);
        var newRefreshToken = _tokenService.GenerateRefreshToken();

        var newRefreshTokenEntity = RefreshTokenEntity.Create(
            userId,
            newRefreshToken,
            request.IpAddress,
            _jwtSettings.RefreshExpiresInDays);

        _context.RefreshTokens.Add(newRefreshTokenEntity);
        await _context.SaveChangesAsync(cancellationToken);

        return new LoginDto(storedToken.User.Name, newAccessToken, newRefreshToken, hotelIds);
    }
}
