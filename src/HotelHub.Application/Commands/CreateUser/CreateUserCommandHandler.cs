using HotelHub.Application.Abstractions;
using HotelHub.Domain.Abstraction;
using HotelHub.Domain.Abstraction.ValueObject;
using HotelHub.Domain.Entities;
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
        var (email, emailError) = EmailAddress.Create(request.Email);

        if (email is null)
            return Error.Validation("Email", emailError!);

        var emailExists = await _context.Users
            .AnyAsync(u => u.Email == email, cancellationToken);

        if (emailExists)
            return Error.Conflict("Email.Duplicate", "Já existe um usuário com esse e-mail.");

        var user = User.Create(request.Name, email, request.Password, request.Role);

        _context.Users.Add(user);
        await _context.SaveChangesAsync(cancellationToken);

        return user.Id;
    }
}
