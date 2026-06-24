using HotelHub.Domain.Abstraction;

namespace HotelHub.Domain.Errors;

public static class UserErrors
{
    public static Error NotFound(Guid id) =>
        Error.NotFound("User", id);

    public static readonly Error EmailDuplicate =
        Error.Conflict("Email.Duplicate", "Já existe um usuário com esse e-mail.");

    public static readonly Error InvalidEmail =
        Error.Validation("Email", "Formato de e-mail inválido.");

    public static readonly Error InvalidCredentials =
        Error.Unauthorized("Credentials.Invalid", "E-mail ou senha inválidos.");

    public static readonly Error NotInHotel =
        Error.Unauthorized("User.NotInHotel", "Usuário não possui acesso a este hotel.");
}
