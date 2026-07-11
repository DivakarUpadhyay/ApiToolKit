using ApiToolKit.Domain.Entities.Facebook;

namespace ApiToolKit.Domain.Interfaces.Repositories.Facebook;

public interface IFacebookAdSetRepository
{
    Task<int> SaveAdSetAsync(FacebookAdSet adSet);
    Task<FacebookAdSet?> GetAdSetByIdAsync(string adSetId);
    Task<IEnumerable<FacebookAdSet>> GetAdSetsByCampaignIdAsync(string campaignId, int limit, int offset);
    Task<int> UpdateAdSetAsync(FacebookAdSet adSet);
    Task<int> DeleteAdSetAsync(string adSetId);
    Task<int> SaveApiLogAsync(FacebookApiLog log);
}
