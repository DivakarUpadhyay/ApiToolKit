using ApiToolKit.Domain.Entities.GoogleAds;

namespace ApiToolKit.Domain.Interfaces.Repositories.GoogleAds;

public interface IGoogleAdsAuthRepository
{
    Task<GoogleAdsConfig?> GetConfigAsync();
    Task<GoogleAdsToken?> GetTokenByCustomerIdAsync(string customerId);
    Task SaveTokenAsync(GoogleAdsToken token);
    Task UpdateTokenAsync(GoogleAdsToken token);
    Task SaveApiLogAsync(GoogleAdsApiLog log);
}
