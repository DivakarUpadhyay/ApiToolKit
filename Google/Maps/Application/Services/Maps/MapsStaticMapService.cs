using ApiToolKit.Application.DTOs.Maps.StaticMap;
using ApiToolKit.Application.Interfaces.Services.Maps;
using ApiToolKit.Domain.Common;
using Microsoft.Extensions.Configuration;

namespace ApiToolKit.Application.Services.Maps;

public class MapsStaticMapService : IMapsStaticMapService
{
    private readonly IConfiguration _configuration;
    private const string BaseUrl = "https://maps.googleapis.com/maps/api/staticmap";

    public MapsStaticMapService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public Task<Result<StaticMapResponse>> GetStaticMapUrlAsync(StaticMapRequest request)
    {
        var apiKey = _configuration["Google:Maps:ApiKey"] ?? string.Empty;
        var sb = new System.Text.StringBuilder($"{BaseUrl}?");
        sb.Append($"center={Uri.EscapeDataString(request.Center)}");
        sb.Append($"&zoom={request.Zoom}");
        sb.Append($"&size={request.Size}");
        sb.Append($"&scale={request.Scale}");
        sb.Append($"&format={request.Format}");
        sb.Append($"&maptype={request.MapType}");
        if (!string.IsNullOrEmpty(request.Language)) sb.Append($"&language={request.Language}");
        foreach (var m in request.Markers) sb.Append($"&markers={Uri.EscapeDataString(m)}");
        foreach (var p in request.Path) sb.Append($"&path={Uri.EscapeDataString(p)}");
        foreach (var v in request.Visible) sb.Append($"&visible={Uri.EscapeDataString(v)}");
        foreach (var s in request.Style) sb.Append($"&style={Uri.EscapeDataString(s)}");
        sb.Append($"&key={apiKey}");

        var parts = request.Size.Split('x');
        var width = parts.Length > 0 && int.TryParse(parts[0], out var w) ? w : 600;
        var height = parts.Length > 1 && int.TryParse(parts[1], out var h) ? h : 400;

        var response = new StaticMapResponse
        {
            ImageUrl = sb.ToString(),
            Width = width,
            Height = height
        };

        return Task.FromResult(Result<StaticMapResponse>.Ok(response));
    }
}
