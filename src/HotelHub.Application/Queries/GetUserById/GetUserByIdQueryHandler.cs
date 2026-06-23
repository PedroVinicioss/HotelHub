using HotelHub.Application.Abstractions;
using HotelHub.Application.Dtos;
using HotelHub.Domain.Abstraction;
using HotelHub.Domain.Errors;
using Microsoft.EntityFrameworkCore;

namespace HotelHub.Application.Queries.GetUserById;

public sealed class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Result<UserDto>>
{
    private readonly IApplicationDbContext _context;

    public GetUserByIdQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<UserDto>> HandleAsync(GetUserByIdQuery request, CancellationToken cancellationToken = default)
    {
        var user = await _context.Users
            .AsNoTracking()
            .Where(u => u.Id == request.UserId && u.IsActive)
            .Select(u => new UserDto(u.Id, u.Name, u.Email.Value, u.UserRole.ToString()))
            .FirstOrDefaultAsync(cancellationToken);

        if (user is null)
            return UserErrors.NotFound(request.UserId);

        return user;
    }
}
