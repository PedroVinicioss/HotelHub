using HotelHub.Application.Abstractions;
using HotelHub.Domain.Abstraction;
using HotelHub.Domain.Abstraction.ValueObject;
using HotelHub.Domain.Entities;

namespace HotelHub.Application.Commands.CreateUser;

public sealed class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<Guid>>
{
    public Task<Result<Guid>> HandleAsync(CreateUserCommand request, CancellationToken cancellationToken = default)
    {
        var (email, emailError) = EmailAddress.Create(request.Email);

        if (email is null)
            return Task.FromResult(Result.Failure<Guid>(Error.Validation("Email", emailError!)));

        var user = User.Create(request.Name, email, request.Password, request.Role);

        // TODO: persistir via repositório

        return Task.FromResult(Result.Success(user.Id));
    }
}
