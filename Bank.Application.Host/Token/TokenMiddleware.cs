namespace Bank.Application.Host.Token;

public class TokenMiddleware 
{
    private readonly RequestDelegate _next;

    public TokenMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Получите токен доступа из сессии
        string accessToken = context.Session.GetString("AccessToken");

        // Если токен доступа существует, установите заголовок Authorization

        context.Request.Headers.TryAdd("Authorization", "Bearer " + accessToken);
        context.Response.Headers.Add("X-SERVICE-NAME", accessToken);

        // Передайте управление следующему компоненту в конвейере
        await _next(context);
    }
}