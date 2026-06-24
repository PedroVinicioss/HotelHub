namespace HotelHub.Api.Security;

public static class RateLimitPolicies
{
    public const string Auth = "auth";       // login / refresh — 5 por minuto por IP
    public const string Standard = "standard"; // endpoints normais — 60 por minuto por IP
}
