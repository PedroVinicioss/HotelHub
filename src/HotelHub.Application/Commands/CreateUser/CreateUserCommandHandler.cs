using HotelHub.Application.Abstractions;
using HotelHub.Application.Security;
using HotelHub.Domain.Abstraction;
using HotelHub.Domain.Abstraction.ValueObject;
using HotelHub.Domain.Entities;
using HotelHub.Domain.Errors;
using Microsoft.EntityFrameworkCore;

namespace HotelHub.Application.Commands.CreateUser;

public sealed class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<Guid>>
{
    private readonly IApplicationDbContext _context;

    public CreateUserCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<Guid>> HandleAsync(CreateUserCommand request, CancellationToken cancellationToken = default)
    {
        var (email, _) = EmailAddress.Create(request.Email);

        if (email is null)
            return UserErrors.InvalidEmail;

        var emailExists = await _context.Users
            .AnyAsync(u => u.Email == email, cancellationToken);

        if (emailExists)
            return UserErrors.EmailDuplicate;

        var passwordHash = PasswordHasher.Hash(request.Password);
        var user = User.Create(request.Name, email, passwordHash, request.Role);

        _context.Users.Add(user);
        await _context.SaveChangesAsync(cancellationToken);

        return user.Id;
    }
}
