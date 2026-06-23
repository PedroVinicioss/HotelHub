using HotelHub.Application.Abstractions;
using HotelHub.Application.Dtos;

namespace HotelHub.Application.Queries.GetUserById;

public sealed record GetUserByIdQuery(Guid UserId) : IQuery<UserDto>;
