namespace HotelHub.Domain.Abstraction;

public sealed record Error(string Code, string Description)
{
    public static readonly Error None = new(string.Empty, string.Empty);
    public static readonly Error NullValue = new("Error.NullValue", "Um valor nulo foi fornecido.");

    public static Error NotFound(string entity, object id) =>
        new($"{entity}.NotFound", $"{entity} com id '{id}' não foi encontrado.");

    public static Error Validation(string field, string message) =>
        new($"Validation.{field}", message);

    public static Error Conflict(string code, string message) =>
        new($"Conflict.{code}", message);
}
