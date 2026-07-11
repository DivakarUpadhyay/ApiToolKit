using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.Maps.StaticMap;

public class StaticMapRequest
{
    [Required]
    public string Center { get; set; } = string.Empty;
    public int Zoom { get; set; } = 14;
    [Required]
    public string Size { get; set; } = "600x400";
    public int Scale { get; set; } = 1;
    public string Format { get; set; } = "png";
    public string MapType { get; set; } = "roadmap";
    public string? Language { get; set; }
    public List<string> Markers { get; set; } = new();
    public List<string> Path { get; set; } = new();
    public List<string> Visible { get; set; } = new();
    public List<string> Style { get; set; } = new();
}

public class StaticMapResponse
{
    public string ImageUrl { get; set; } = string.Empty;
    public int Width { get; set; }
    public int Height { get; set; }
}
