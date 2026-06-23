using HotelHub.Application.Abstractions;
using HotelHub.Domain.Enums;

namespace HotelHub.Application.Commands.CreateUser;

public sealed record CreateUserCommand(
    string Name,
    string Email,
    string Password,
    UserRole Role
) : ICommand<Guid>;
