using HotelHub.Application.Abstractions;
using HotelHub.Domain.Abstraction.ValueObject;
using HotelHub.Domain.Entities;

namespace HotelHub.Application.Commands.CreateUser;

public sealed class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
{
    public Task<Guid> HandleAsync(CreateUserCommand request, CancellationToken cancellationToken = default)
    {
        var (email, error) = EmailAddress.Create(request.Email);

        if (email is null)
            throw new ArgumentException(error, nameof(request.Email));

        var user = User.Create(request.Name, email, request.Password, request.Role);

        // TODO: persistir via repositório

        return Task.FromResult(user.Id);
    }
}
