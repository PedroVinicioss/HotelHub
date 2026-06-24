namespace HotelHub.Api.Security;

public sealed class SecurityHeadersMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        var headers = context.Response.Headers;

        // Impede que o browser "adivinhe" o content-type (MIME sniffing)
        headers["X-Content-Type-Options"] = "nosniff";

        // Bloqueia a página dentro de iframes — previne clickjacking
        headers["X-Frame-Options"] = "DENY";

        // Não vazar referrer para origens externas
        headers["Referrer-Policy"] = "strict-origin-when-cross-origin";

        // Desativa funcionalidades do browser que não precisamos
        headers["Permissions-Policy"] = "camera=(), microphone=(), geolocation=(), payment=()";

        // Remove headers que identificam o servidor/tecnologia
        headers.Remove("Server");
        headers.Remove("X-Powered-By");
        headers.Remove("X-AspNet-Version");

        await next(context);
    }
}
