namespace HotelHub.Application.Settings;

public sealed class JwtSettings
{
    public const string SectionName = "JwtSettings";

    public string Secret { get; init; } = string.Empty;
    public int ExpiresInMinutes { get; init; } = 60;
    public int RefreshExpiresInDays { get; init; } = 7;
}
