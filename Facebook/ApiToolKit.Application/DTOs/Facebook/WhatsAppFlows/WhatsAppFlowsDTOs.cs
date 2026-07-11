using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.Facebook.WhatsAppFlows;

public class CreateWhatsAppFlowRequest
{
    [Required] public string WabaId { get; set; } = string.Empty;
    [Required] public string Name { get; set; } = string.Empty;
    [Required] public List<string> Categories { get; set; } = new();
    public string? EndpointUri { get; set; }
    public string? FlowJson { get; set; }
}

public class WhatsAppFlowResponse
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? Status { get; set; }
    public List<string>? Categories { get; set; }
    [Newtonsoft.Json.JsonProperty("validation_errors")] public List<string>? ValidationErrors { get; set; }
    [Newtonsoft.Json.JsonProperty("json_version")] public string? JsonVersion { get; set; }
    [Newtonsoft.Json.JsonProperty("data_api_version")] public string? DataApiVersion { get; set; }
    [Newtonsoft.Json.JsonProperty("endpoint_uri")] public string? EndpointUri { get; set; }
    [Newtonsoft.Json.JsonProperty("preview")] public WhatsAppFlowPreview? Preview { get; set; }
    [Newtonsoft.Json.JsonProperty("whatsapp_business_account")] public WhatsAppFlowWaba? WhatsAppBusinessAccount { get; set; }
}

public class WhatsAppFlowPreview
{
    [Newtonsoft.Json.JsonProperty("preview_url")] public string? PreviewUrl { get; set; }
    [Newtonsoft.Json.JsonProperty("expires_at")] public string? ExpiresAt { get; set; }
}

public class WhatsAppFlowWaba
{
    public string? Id { get; set; }
}

public class UpdateWhatsAppFlowRequest
{
    [Required] public string FlowId { get; set; } = string.Empty;
    public string? Name { get; set; }
    public List<string>? Categories { get; set; }
    public string? EndpointUri { get; set; }
    public string? ApplicationId { get; set; }
}

public class GetWhatsAppFlowsRequest
{
    [Required] public string WabaId { get; set; } = string.Empty;
    public int Limit { get; set; } = 25;
    public string? After { get; set; }
}

public class UploadFlowJsonRequest
{
    [Required] public string FlowId { get; set; } = string.Empty;
    [Required] public string FlowJson { get; set; } = string.Empty;
}

public class WhatsAppFlowAsset
{
    [Newtonsoft.Json.JsonProperty("asset_type")] public string? AssetType { get; set; }
    [Newtonsoft.Json.JsonProperty("download_url")] public string? DownloadUrl { get; set; }
    public string? Name { get; set; }
}
