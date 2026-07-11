using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.Shopee.Auth;

public class GetAccessTokenRequest
{
    [Required] public string Code { get; set; } = string.Empty;
    [Required] public long ShopId { get; set; }
}

public class RefreshAccessTokenRequest
{
    [Required] public string RefreshToken { get; set; } = string.Empty;
    [Required] public long ShopId { get; set; }
}

public class AccessTokenResponse
{
    public string AccessToken { get; set; } = string.Empty;
    public long ExpireIn { get; set; }
    public string RefreshToken { get; set; } = string.Empty;
    public long RefreshTokenExpireIn { get; set; }
    public long ShopId { get; set; }
    public long PartnerId { get; set; }
    public string Error { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public string RequestId { get; set; } = string.Empty;
}

public class ShopeeAuthUrlResponse
{
    public string AuthUrl { get; set; } = string.Empty;
}
