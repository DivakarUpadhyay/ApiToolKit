using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace ApiToolKit.Application.DTOs.Firebase.RemoteConfig;

public class RemoteConfigCondition
{
    [Required]
    [JsonProperty("name")]
    public string Name { get; set; } = string.Empty;

    [Required]
    [JsonProperty("expression")]
    public string Expression { get; set; } = string.Empty;

    [JsonProperty("tagColor")]
    public string? TagColor { get; set; }
}

public class RemoteConfigParameterValue
{
    [JsonProperty("value")]
    public string? Value { get; set; }

    [JsonProperty("useInAppDefault")]
    public bool? UseInAppDefault { get; set; }
}

public class RemoteConfigParameter
{
    [JsonProperty("defaultValue")]
    public RemoteConfigParameterValue? DefaultValue { get; set; }

    [JsonProperty("conditionalValues")]
    public Dictionary<string, RemoteConfigParameterValue>? ConditionalValues { get; set; }

    [JsonProperty("description")]
    public string? Description { get; set; }

    [JsonProperty("valueType")]
    public string ValueType { get; set; } = "STRING";
}

public class RemoteConfigVersion
{
    [JsonProperty("versionNumber")]
    public string? VersionNumber { get; set; }

    [JsonProperty("updateTime")]
    public string? UpdateTime { get; set; }

    [JsonProperty("updateUser")]
    public object? UpdateUser { get; set; }

    [JsonProperty("updateOrigin")]
    public string? UpdateOrigin { get; set; }

    [JsonProperty("updateType")]
    public string? UpdateType { get; set; }

    [JsonProperty("description")]
    public string? Description { get; set; }

    [JsonProperty("isLegacy")]
    public bool IsLegacy { get; set; }
}

public class RemoteConfigTemplate
{
    [Required]
    [JsonProperty("parameters")]
    public Dictionary<string, RemoteConfigParameter> Parameters { get; set; } = new();

    [JsonProperty("conditions")]
    public List<RemoteConfigCondition>? Conditions { get; set; }

    [JsonProperty("version")]
    public RemoteConfigVersion? Version { get; set; }
}

public class GetRemoteConfigRequest
{
    [Required]
    public string ProjectId { get; set; } = string.Empty;

    [Required]
    public string AccessToken { get; set; } = string.Empty;
}

public class GetRemoteConfigResponse
{
    [JsonProperty("conditions")]
    public List<RemoteConfigCondition>? Conditions { get; set; }

    [JsonProperty("parameters")]
    public Dictionary<string, RemoteConfigParameter>? Parameters { get; set; }

    [JsonProperty("version")]
    public RemoteConfigVersion? Version { get; set; }

    public string? ETag { get; set; }
}

public class UpdateRemoteConfigRequest
{
    [Required]
    public string ProjectId { get; set; } = string.Empty;

    [Required]
    public string AccessToken { get; set; } = string.Empty;

    [Required]
    public RemoteConfigTemplate Template { get; set; } = new();

    [Required]
    public string ETag { get; set; } = string.Empty;

    public bool ValidateOnly { get; set; } = false;
}

public class ListRemoteConfigVersionsRequest
{
    [Required]
    public string ProjectId { get; set; } = string.Empty;

    [Required]
    public string AccessToken { get; set; } = string.Empty;

    public int PageSize { get; set; } = 10;
    public string? PageToken { get; set; }
    public string? StartTime { get; set; }
    public string? EndTime { get; set; }
}

public class RollbackRemoteConfigRequest
{
    [Required]
    public string ProjectId { get; set; } = string.Empty;

    [Required]
    public string AccessToken { get; set; } = string.Empty;

    [Required]
    public string VersionNumber { get; set; } = string.Empty;
}
