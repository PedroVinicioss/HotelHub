namespace HotelHub.Domain.Abstraction;

public enum ErrorType
{
    Validation,
    NotFound,
    Conflict,
    Unauthorized
}

public sealed record Error(string Code, string Description, ErrorType Type)
{
    public static readonly Error None = new(string.Empty, string.Empty, ErrorType.Validation);
    public static readonly Error NullValue = new("Error.NullValue", "Um valor nulo foi fornecido.", ErrorType.Validation);

    public static Error NotFound(string entity, object id) =>
        new($"{entity}.NotFound", $"{entity} com id '{id}' não foi encontrado.", ErrorType.NotFound);

    public static Error Validation(string field, string message) =>
        new($"Validation.{field}", message, ErrorType.Validation);

    public static Error Conflict(string code, string message) =>
        new($"Conflict.{code}", message, ErrorType.Conflict);

    public static Error Unauthorized(string code, string message) =>
        new($"Unauthorized.{code}", message, ErrorType.Unauthorized);
}
