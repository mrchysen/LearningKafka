namespace OrderService.Infrastructure;

public class LoggingMiddleware(
    RequestDelegate next,
    IWebHostEnvironment env,
    ILogger<LoggingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        using (logger.BeginScope(new Dictionary<string, object>
        {
            ["Application"] = env.ApplicationName,
            ["TraceId"] = context.TraceIdentifier
        }))
        {
            await next(context);
        }
    }
}
