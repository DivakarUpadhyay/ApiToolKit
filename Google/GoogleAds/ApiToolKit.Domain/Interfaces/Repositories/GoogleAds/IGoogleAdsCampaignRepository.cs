using ApiToolKit.Domain.Entities.GoogleAds;

namespace ApiToolKit.Domain.Interfaces.Repositories.GoogleAds;

public interface IGoogleAdsCampaignRepository
{
    Task SaveCampaignAsync(GoogleAdsCampaign campaign);
    Task UpdateCampaignAsync(GoogleAdsCampaign campaign);
    Task<IEnumerable<GoogleAdsCampaign>> GetByCustomerIdAsync(string customerId);
}
