using System.Net;
using System.Text.Json;

namespace ApiToolKit.Gateway;

/// <summary>Forwards /api/* requests to the module that owns the path, so "Try it out" works from one page.</summary>
public class ModuleProxy
{
    private static readonly HashSet<string> HopByHop = new(StringComparer.OrdinalIgnoreCase)
    {
        "Connection", "Keep-Alive", "Proxy-Connection", "Transfer-Encoding", "Upgrade", "TE", "Trailer", "Host"
    };

    private readonly SpecCatalog _catalog;
    private readonly IHttpClientFactory _httpFactory;

    public ModuleProxy(SpecCatalog catalog, IHttpClientFactory httpFactory)
    {
        _catalog = catalog;
        _httpFactory = httpFactory;
    }

    public async Task HandleAsync(HttpContext context)
    {
        var path = context.Request.Path.Value ?? "/";
        var module = _catalog.Route(path);
        if (module == null)
        {
            await WriteError(context, HttpStatusCode.NotFound, $"No ApiToolKit module serves {path}. Open the module's group once (or start the module) so the gateway learns its routes.");
            return;
        }

        var target = module.BaseUrl.TrimEnd('/') + path + context.Request.QueryString;
        using var request = new HttpRequestMessage(new HttpMethod(context.Request.Method), target);

        if (context.Request.ContentLength > 0 || context.Request.Headers.ContainsKey("Transfer-Encoding"))
            request.Content = new StreamContent(context.Request.Body);

        foreach (var header in context.Request.Headers)
        {
            if (HopByHop.Contains(header.Key)) continue;
            if (!request.Headers.TryAddWithoutValidation(header.Key, header.Value.ToArray()))
                request.Content?.Headers.TryAddWithoutValidation(header.Key, header.Value.ToArray());
        }

        HttpResponseMessage response;
        try
        {
            response = await _httpFactory.CreateClient("proxy").SendAsync(request, HttpCompletionOption.ResponseHeadersRead, context.RequestAborted);
        }
        catch (HttpRequestException)
        {
            await WriteError(context, HttpStatusCode.ServiceUnavailable,
                $"Module '{module.Name}' is not running. Start it with: .\\Gateway\\start-modules.ps1 {module.Key}  (listens on {module.BaseUrl})");
            return;
        }

        using (response)
        {
            context.Response.StatusCode = (int)response.StatusCode;
            foreach (var header in response.Headers.Concat(response.Content.Headers))
                if (!HopByHop.Contains(header.Key))
                    context.Response.Headers[header.Key] = header.Value.ToArray();
            await response.Content.CopyToAsync(context.Response.Body, context.RequestAborted);
        }
    }

    private static async Task WriteError(HttpContext context, HttpStatusCode status, string message)
    {
        context.Response.StatusCode = (int)status;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(JsonSerializer.Serialize(new { success = false, data = (object?)null, message }));
    }
}
