using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace ApiToolKit.Application.DTOs.BusinessProfile.Auth;

public class GetAuthUrlResponse
{
    public string AuthUrl { get; set; } = string.Empty;
}

public class BusinessProfileTokenResponse
{
    [JsonProperty("access_token")] public string AccessToken { get; set; } = string.Empty;
    [JsonProperty("refresh_token")] public string? RefreshToken { get; set; }
    [JsonProperty("expires_in")] public int ExpiresIn { get; set; }
    [JsonProperty("token_type")] public string TokenType { get; set; } = string.Empty;
    [JsonProperty("scope")] public string? Scope { get; set; }
    [JsonProperty("error")] public string? Error { get; set; }
    [JsonProperty("error_description")] public string? ErrorDescription { get; set; }
}

public class RefreshTokenRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
}
