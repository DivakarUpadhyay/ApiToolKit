using ApiToolKit.Domain.Entities.GoogleAds;

namespace ApiToolKit.Domain.Interfaces.Repositories.GoogleAds;

public interface IGoogleAdsAdGroupRepository
{
    Task SaveAdGroupAsync(GoogleAdsAdGroup adGroup);
    Task UpdateAdGroupAsync(GoogleAdsAdGroup adGroup);
    Task<IEnumerable<GoogleAdsAdGroup>> GetByCampaignAsync(string customerId, string campaignResourceName);
}
