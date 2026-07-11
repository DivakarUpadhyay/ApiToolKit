using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.Analytics.Auth;

public class GetAuthUrlResponse
{
    public string AuthUrl { get; set; } = string.Empty;
}

public class ExchangeCodeRequest
{
    [Required]
    public string Code { get; set; } = string.Empty;
    [Required]
    public string PropertyId { get; set; } = string.Empty;
}

public class AnalyticsTokenResponse
{
    public string AccessToken { get; set; } = string.Empty;
    public string? RefreshToken { get; set; }
    public int ExpiresIn { get; set; }
    public string TokenType { get; set; } = string.Empty;
    public string? Scope { get; set; }
    public string? Error { get; set; }
    public string? ErrorDescription { get; set; }
}

public class RefreshTokenRequest
{
    [Required]
    public string PropertyId { get; set; } = string.Empty;
}
