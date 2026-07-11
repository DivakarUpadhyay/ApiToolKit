using ApiToolKit.Domain.Entities.GoogleAds;

namespace ApiToolKit.Domain.Interfaces.Repositories.GoogleAds;

public interface IGoogleAdsKeywordRepository
{
    Task SaveKeywordAsync(GoogleAdsKeyword keyword);
    Task UpdateKeywordAsync(GoogleAdsKeyword keyword);
    Task<IEnumerable<GoogleAdsKeyword>> GetByAdGroupAsync(string customerId, string adGroupResourceName);
}
