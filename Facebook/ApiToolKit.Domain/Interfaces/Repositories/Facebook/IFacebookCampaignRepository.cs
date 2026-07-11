using ApiToolKit.Domain.Entities.Facebook;

namespace ApiToolKit.Domain.Interfaces.Repositories.Facebook;

public interface IFacebookCampaignRepository
{
    Task<int> SaveCampaignAsync(FacebookCampaign campaign);
    Task<FacebookCampaign?> GetCampaignByIdAsync(string campaignId);
    Task<IEnumerable<FacebookCampaign>> GetCampaignsByAccountIdAsync(string accountId, int limit, int offset);
    Task<int> UpdateCampaignAsync(FacebookCampaign campaign);
    Task<int> DeleteCampaignAsync(string campaignId);
    Task<int> SaveApiLogAsync(FacebookApiLog log);
}
