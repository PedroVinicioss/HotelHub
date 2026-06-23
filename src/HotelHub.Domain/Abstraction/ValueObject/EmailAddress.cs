using System.Text.RegularExpressions;

namespace HotelHub.Domain.Abstraction.ValueObject;

public sealed class EmailAddress : IEquatable<EmailAddress>
{
    private static readonly Regex EmailRegex = new(
        @"^[^@\s]+@[^@\s]+\.[^@\s]+$", 
        RegexOptions.Compiled | RegexOptions.IgnoreCase);

    public string Value { get; }

    // Private constructor ensures the object can ONLY be created via valid state
    private EmailAddress(string value)
    {
        Value = value;
    }

    public static (EmailAddress? Email, string? Error) Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return (null, "Email cannot be empty.");

        var trimmed = value.Trim().ToLowerInvariant();

        if (!EmailRegex.IsMatch(trimmed))
            return (null, "Invalid email format.");

        return (new EmailAddress(trimmed), null);
    }

    // Structural Equality Implementation
    public bool Equals(EmailAddress? other) => other is not null && Value == other.Value;
    public override bool Equals(object? obj) => Equals(obj as EmailAddress);
    public override int GetHashCode() => Value.GetHashCode();
    public static bool operator ==(EmailAddress? left, EmailAddress? right) => Equals(left, right);
    public static bool operator !=(EmailAddress? left, EmailAddress? right) => !Equals(left, right);

    public override string ToString() => Value;
}