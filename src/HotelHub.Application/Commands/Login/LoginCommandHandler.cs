using HotelHub.Application.Abstractions;
using HotelHub.Application.Dtos;
using HotelHub.Application.Security;
using HotelHub.Domain.Abstraction;
using HotelHub.Domain.Abstraction.ValueObject;
using HotelHub.Domain.Errors;
using Microsoft.EntityFrameworkCore;

namespace HotelHub.Application.Commands.Login;

public sealed class LoginCommandHandler : IRequestHandler<LoginCommand, Result<LoginDto>>
{
    private readonly IApplicationDbContext _context;

    public LoginCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<LoginDto>> HandleAsync(LoginCommand request, CancellationToken cancellationToken = default)
    {
        var (email, emailError) = EmailAddress.Create(request.Email);

        if (email is null)
            return UserErrors.InvalidEmail;

        var user = await _context.Users
            .Include(u => u.UserHotels)
            .FirstOrDefaultAsync(u => u.Email == email && u.IsActive, cancellationToken);

        if (user is null || !PasswordHasher.Verify(request.Password, user.Password))
            return UserErrors.InvalidCredentials;

        var hotelIds = user.UserHotels.Select(uh => uh.HotelId).ToList();

        // TODO: gerar JWT via ITokenService quando implementar autenticação
        return new LoginDto(user.Name, string.Empty, null, hotelIds);
    }
}
