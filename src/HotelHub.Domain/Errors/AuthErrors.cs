using HotelHub.Domain.Abstraction;

namespace HotelHub.Domain.Errors;

public static class AuthErrors
{
    public static readonly Error InvalidRefreshToken =
        Error.Unauthorized("RefreshToken.Invalid", "Token de atualização inválido ou expirado.");
}
