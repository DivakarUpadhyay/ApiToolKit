using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.Shopify.Auth;

public class GetInstallUrlRequest
{
    [Required] public string ShopDomain { get; set; } = string.Empty;
    public string? State { get; set; }
}

public class GetInstallUrlResponse
{
    public string InstallUrl { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
}

public class CallbackRequest
{
    [Required] public string Code { get; set; } = string.Empty;
    [Required] public string Shop { get; set; } = string.Empty;
    [Required] public string Hmac { get; set; } = string.Empty;
    [Required] public string State { get; set; } = string.Empty;
    [Required] public string Timestamp { get; set; } = string.Empty;
}

public class CallbackResponse
{
    public string ShopDomain { get; set; } = string.Empty;
    public string AccessToken { get; set; } = string.Empty;
    public string Scope { get; set; } = string.Empty;
}

public class ShopInfoResponse
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Domain { get; set; } = string.Empty;
    public string MyshopifyDomain { get; set; } = string.Empty;
    public string PlanName { get; set; } = string.Empty;
    public string Currency { get; set; } = string.Empty;
    public string CountryCode { get; set; } = string.Empty;
    public string TimezoneAbbreviation { get; set; } = string.Empty;
    public string CreatedAt { get; set; } = string.Empty;
}

public class VerifyHmacRequest
{
    [Required] public string QueryString { get; set; } = string.Empty;
}

public class ShopifyTokenResponse
{
    public string ShopDomain { get; set; } = string.Empty;
    public string AccessToken { get; set; } = string.Empty;
    public string Scope { get; set; } = string.Empty;
    public string TokenType { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}
