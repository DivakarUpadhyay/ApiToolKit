using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.Facebook.Auth;

public class GetAuthUrlRequest
{
    public List<string>? Scope { get; set; }
    public string? State { get; set; }
}

public class GetAuthUrlResponse
{
    public string AuthUrl { get; set; } = string.Empty;
}

public class ExchangeCodeRequest
{
    [Required] public string Code { get; set; } = string.Empty;
    public string? State { get; set; }
}

public class ExchangeCodeResponse
{
    public string AccessToken { get; set; } = string.Empty;
    public string TokenType { get; set; } = string.Empty;
    public int ExpiresIn { get; set; }
    public string? Scope { get; set; }
}

public class RefreshTokenRequest
{
    [Required] public string AccessToken { get; set; } = string.Empty;
}

public class DebugTokenRequest
{
    [Required] public string InputToken { get; set; } = string.Empty;
}

public class DebugTokenResponse
{
    public string AppId { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public bool IsValid { get; set; }
    public long ExpiresAt { get; set; }
    public List<string> Scopes { get; set; } = new();
}

public class GetAppTokenResponse
{
    public string AccessToken { get; set; } = string.Empty;
    public string TokenType { get; set; } = string.Empty;
}

public class LongLivedTokenRequest
{
    [Required] public string ShortLivedToken { get; set; } = string.Empty;
}

public class LongLivedTokenResponse
{
    public string AccessToken { get; set; } = string.Empty;
    public string TokenType { get; set; } = string.Empty;
    public int ExpiresIn { get; set; }
}
