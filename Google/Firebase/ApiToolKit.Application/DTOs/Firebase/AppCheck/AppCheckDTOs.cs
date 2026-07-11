using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace ApiToolKit.Application.DTOs.Firebase.AppCheck;

public class AppCheckTokenResponse
{
    [JsonProperty("token")]
    public string? Token { get; set; }

    [JsonProperty("ttl")]
    public string? Ttl { get; set; }
}

public class VerifyAppCheckTokenRequest
{
    [Required]
    public string ProjectId { get; set; } = string.Empty;

    [Required]
    public string AppCheckToken { get; set; } = string.Empty;

    [Required]
    public string AccessToken { get; set; } = string.Empty;
}

public class VerifyAppCheckTokenResponse
{
    [JsonProperty("appId")]
    public string? AppId { get; set; }

    [JsonProperty("alreadyConsumed")]
    public bool? AlreadyConsumed { get; set; }
}

public class BatchGetAppCheckConfigsRequest
{
    [Required]
    public string ProjectId { get; set; } = string.Empty;

    [Required]
    public List<string> AppIds { get; set; } = new();

    [Required]
    public string AccessToken { get; set; } = string.Empty;
}

public class AppCheckConfig
{
    [JsonProperty("name")]
    public string? Name { get; set; }

    [JsonProperty("serviceName")]
    public string? ServiceName { get; set; }

    [JsonProperty("enforcementMode")]
    public string EnforcementMode { get; set; } = "UNENFORCED";
}

public class BatchUpdateAppCheckConfigsRequest
{
    [Required]
    public string ProjectId { get; set; } = string.Empty;

    [Required]
    public string AccessToken { get; set; } = string.Empty;

    [Required]
    public List<AppCheckConfig> Configs { get; set; } = new();
}
