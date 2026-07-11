using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace ApiToolKit.Application.DTOs.Identity.OAuth;

public class GetAuthUrlResponse
{
    public string AuthUrl { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
}

public class AuthorizationRequest
{
    [Required]
    public List<string> Scopes { get; set; } = new();
    public string? RedirectUri { get; set; }
    public string? State { get; set; }
    public string Prompt { get; set; } = "consent";
    public string AccessType { get; set; } = "offline";
    public bool IncludeGrantedScopes { get; set; } = false;
}

public class OAuthTokenResponse
{
    [JsonProperty("access_token")]
    public string? AccessToken { get; set; }

    [JsonProperty("refresh_token")]
    public string? RefreshToken { get; set; }

    [JsonProperty("id_token")]
    public string? IdToken { get; set; }

    [JsonProperty("expires_in")]
    public int ExpiresIn { get; set; }

    [JsonProperty("token_type")]
    public string? TokenType { get; set; }

    [JsonProperty("scope")]
    public string? Scope { get; set; }
}

public class RefreshTokenRequest
{
    [Required]
    public string RefreshToken { get; set; } = string.Empty;
}

public class RevokeTokenRequest
{
    [Required]
    public string Token { get; set; } = string.Empty;
}

public class TokenInfoResponse
{
    [JsonProperty("iss")]
    public string? Iss { get; set; }

    [JsonProperty("azp")]
    public string? Azp { get; set; }

    [JsonProperty("aud")]
    public string? Aud { get; set; }

    [JsonProperty("sub")]
    public string? Sub { get; set; }

    [JsonProperty("email")]
    public string? Email { get; set; }

    [JsonProperty("email_verified")]
    public string? EmailVerified { get; set; }

    [JsonProperty("at_hash")]
    public string? AtHash { get; set; }

    [JsonProperty("name")]
    public string? Name { get; set; }

    [JsonProperty("picture")]
    public string? Picture { get; set; }

    [JsonProperty("given_name")]
    public string? GivenName { get; set; }

    [JsonProperty("family_name")]
    public string? FamilyName { get; set; }

    [JsonProperty("iat")]
    public string? Iat { get; set; }

    [JsonProperty("exp")]
    public string? Exp { get; set; }

    [JsonProperty("alg")]
    public string? Alg { get; set; }

    [JsonProperty("kid")]
    public string? Kid { get; set; }

    [JsonProperty("typ")]
    public string? Typ { get; set; }
}

public class UserInfoResponse
{
    [JsonProperty("sub")]
    public string? Sub { get; set; }

    [JsonProperty("name")]
    public string? Name { get; set; }

    [JsonProperty("given_name")]
    public string? GivenName { get; set; }

    [JsonProperty("family_name")]
    public string? FamilyName { get; set; }

    [JsonProperty("email")]
    public string? Email { get; set; }

    [JsonProperty("email_verified")]
    public bool EmailVerified { get; set; }

    [JsonProperty("picture")]
    public string? Picture { get; set; }

    [JsonProperty("locale")]
    public string? Locale { get; set; }

    [JsonProperty("hd")]
    public string? Hd { get; set; }
}

public class IntrospectTokenRequest
{
    [Required]
    public string Token { get; set; } = string.Empty;
    public string? TokenTypeHint { get; set; }
}
