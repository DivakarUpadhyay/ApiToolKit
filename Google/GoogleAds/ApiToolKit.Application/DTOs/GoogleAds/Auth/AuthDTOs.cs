using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.GoogleAds.Auth;

public class GetAuthUrlResponse
{
    public string AuthUrl { get; set; } = string.Empty;
}

public class ExchangeCodeRequest
{
    [Required] public string Code { get; set; } = string.Empty;
    [Required] public string RedirectUri { get; set; } = string.Empty;
}

public class GoogleTokenResponse
{
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public int ExpiresIn { get; set; }
    public string TokenType { get; set; } = string.Empty;
    public string Scope { get; set; } = string.Empty;
}

public class RefreshTokenRequest
{
    [Required] public string RefreshToken { get; set; } = string.Empty;
    [Required] public string CustomerId { get; set; } = string.Empty;
}
