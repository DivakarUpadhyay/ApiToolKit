namespace ApiToolKit.Gateway;

public class GatewayOptions
{
    public List<ModuleOptions> Modules { get; set; } = new();
    public List<GroupOptions> Groups { get; set; } = new();
}

/// <summary>One ApiToolKit web app (it has its own port and its own swagger.json).</summary>
public class ModuleOptions
{
    public string Key { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    /// <summary>Web project folder, relative to the ApiToolKit repo root.</summary>
    public string Project { get; set; } = string.Empty;
    public string BaseUrl { get; set; } = string.Empty;
    public string SwaggerPath { get; set; } = "/swagger/v1/swagger.json";
}

/// <summary>One entry in the Swagger "Select a definition" dropdown.</summary>
public class GroupOptions
{
    public string Key { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public List<GroupSource> Sources { get; set; } = new();
}

public class GroupSource
{
    public string Module { get; set; } = string.Empty;
    /// <summary>Regex: keep only operations with a matching tag.</summary>
    public string? IncludeTags { get; set; }
    /// <summary>Regex: drop operations with a matching tag.</summary>
    public string? ExcludeTags { get; set; }
    /// <summary>Prefix added to every tag (used when several modules share a group).</summary>
    public string? TagPrefix { get; set; }
}
