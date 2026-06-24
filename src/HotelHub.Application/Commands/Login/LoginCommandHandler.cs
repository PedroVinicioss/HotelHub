using HotelHub.Application.Abstractions;
using HotelHub.Application.Dtos;
using HotelHub.Application.Security;
using HotelHub.Application.Settings;
using HotelHub.Domain.Abstraction;
using HotelHub.Domain.Abstraction.ValueObject;
using HotelHub.Domain.Entities;
using RefreshTokenEntity = HotelHub.Domain.Entities.RefreshToken;
using HotelHub.Domain.Errors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace HotelHub.Application.Commands.Login;

public sealed class LoginCommandHandler : IRequestHandler<LoginCommand, Result<LoginDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly ITokenService _tokenService;
    private readonly JwtSettings _jwtSettings;

    public LoginCommandHandler(
        IApplicationDbContext context,
        ITokenService tokenService,
        IOptions<JwtSettings> jwtSettings)
    {
        _context = context;
        _tokenService = tokenService;
        _jwtSettings = jwtSettings.Value;
    }

    public async Task<Result<LoginDto>> HandleAsync(LoginCommand request, CancellationToken cancellationToken = default)
    {
        var (email, _) = EmailAddress.Create(request.Email);

        if (email is null)
            return UserErrors.InvalidEmail;

        var user = await _context.Users
            .Include(u => u.UserHotels)
            .FirstOrDefaultAsync(u => u.Email == email && u.IsActive, cancellationToken);

        if (user is null || !PasswordHasher.Verify(request.Password, user.Password))
            return UserErrors.InvalidCredentials;

        List<Guid> tokenHotelIds;

        if (request.HotelId.HasValue)
        {
            if (!user.UserHotels.Any(uh => uh.HotelId == request.HotelId.Value))
                return UserErrors.NotInHotel;

            tokenHotelIds = [request.HotelId.Value];
        }
        else
        {
            tokenHotelIds = [];
        }

        var accessToken = _tokenService.GenerateAccessToken(user, tokenHotelIds);

        string? refreshToken = null;

        if (request.RememberMe)
        {
            refreshToken = _tokenService.GenerateRefreshToken();

            var refreshTokenEntity = RefreshTokenEntity.Create(
                user.Id,
                refreshToken,
                request.IpAddress,
                _jwtSettings.RefreshExpiresInDays);

            _context.RefreshTokens.Add(refreshTokenEntity);
            await _context.SaveChangesAsync(cancellationToken);
        }

        return new LoginDto(user.Name, accessToken, refreshToken, tokenHotelIds);
    }
}
