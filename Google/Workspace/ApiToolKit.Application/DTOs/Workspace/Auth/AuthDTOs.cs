using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.Workspace.Auth;

public class GetAuthUrlResponse
{
    public string AuthUrl { get; set; } = string.Empty;
}

public class ExchangeCodeRequest
{
    [Required]
    public string Code { get; set; } = string.Empty;

    [Required]
    public string RedirectUri { get; set; } = string.Empty;
}

public class WorkspaceTokenResponse
{
    public string AccessToken { get; set; } = string.Empty;
    public string? RefreshToken { get; set; }
    public int ExpiresIn { get; set; }
    public string TokenType { get; set; } = "Bearer";
    public string? Scope { get; set; }
}

public class RefreshTokenRequest
{
    [Required]
    public string RefreshToken { get; set; } = string.Empty;

    [Required]
    public string UserId { get; set; } = string.Empty;
}
