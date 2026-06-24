using System.Threading.RateLimiting;
using HotelHub.Api.Security;
using Microsoft.AspNetCore.RateLimiting;

namespace HotelHub.Api.Extensions;

public static class RateLimitingExtensions
{
    public static IServiceCollection AddApiRateLimiting(this IServiceCollection services)
    {
        services.AddRateLimiter(options =>
        {
            options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

            // Global: 100 requisições por minuto por IP — defesa geral contra DDoS
            options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(ctx =>
                RateLimitPartition.GetFixedWindowLimiter(
                    partitionKey: GetClientIp(ctx),
                    factory: _ => new FixedWindowRateLimiterOptions
                    {
                        PermitLimit = 100,
                        Window = TimeSpan.FromMinutes(1),
                        QueueLimit = 0
                    }));

            // Auth: 5 tentativas por minuto por IP — anti brute-force no login/refresh
            options.AddPolicy(RateLimitPolicies.Auth, ctx =>
                RateLimitPartition.GetFixedWindowLimiter(
                    partitionKey: GetClientIp(ctx),
                    factory: _ => new FixedWindowRateLimiterOptions
                    {
                        PermitLimit = 5,
                        Window = TimeSpan.FromMinutes(1),
                        QueueLimit = 0
                    }));

            // Standard: 60 requisições por minuto por IP — para endpoints normais
            options.AddPolicy(RateLimitPolicies.Standard, ctx =>
                RateLimitPartition.GetSlidingWindowLimiter(
                    partitionKey: GetClientIp(ctx),
                    factory: _ => new SlidingWindowRateLimiterOptions
                    {
                        PermitLimit = 60,
                        Window = TimeSpan.FromMinutes(1),
                        SegmentsPerWindow = 4,
                        QueueLimit = 0
                    }));

            options.OnRejected = async (context, token) =>
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                context.HttpContext.Response.ContentType = "application/json";

                var retryAfter = context.Lease.TryGetMetadata(MetadataName.RetryAfter, out var retry)
                    ? (int)retry.TotalSeconds
                    : 60;

                context.HttpContext.Response.Headers["Retry-After"] = retryAfter.ToString();

                await context.HttpContext.Response.WriteAsJsonAsync(new
                {
                    code = "RateLimit.Exceeded",
                    description = "Muitas requisições. Tente novamente em instantes.",
                    retryAfterSeconds = retryAfter
                }, token);
            };
        });

        return services;
    }

    private static string GetClientIp(HttpContext ctx) =>
        ctx.Connection.RemoteIpAddress?.ToString() ?? "unknown";
}
