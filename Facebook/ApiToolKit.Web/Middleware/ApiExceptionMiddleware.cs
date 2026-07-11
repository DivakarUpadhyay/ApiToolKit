using Newtonsoft.Json;

namespace ApiToolKit.Web.Middleware;

public class ApiExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ApiExceptionMiddleware> _logger;

    public ApiExceptionMiddleware(RequestDelegate next, ILogger<ApiExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception for {Method} {Path}", context.Request.Method, context.Request.Path);
            context.Response.StatusCode = 200;
            context.Response.ContentType = "application/json";
            var body = JsonConvert.SerializeObject(new
            {
                success = false,
                data = (object?)null,
                message = ex.Message,
                errorCode = (string?)null
            });
            await context.Response.WriteAsync(body);
        }
    }
}
