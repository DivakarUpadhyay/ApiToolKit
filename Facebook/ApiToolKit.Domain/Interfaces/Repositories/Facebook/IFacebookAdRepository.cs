using ApiToolKit.Domain.Entities.Facebook;

namespace ApiToolKit.Domain.Interfaces.Repositories.Facebook;

public interface IFacebookAdRepository
{
    Task<int> SaveAdAsync(FacebookAd ad);
    Task<FacebookAd?> GetAdByIdAsync(string adId);
    Task<IEnumerable<FacebookAd>> GetAdsByAdSetIdAsync(string adSetId, int limit, int offset);
    Task<int> UpdateAdAsync(FacebookAd ad);
    Task<int> DeleteAdAsync(string adId);
    Task<int> SaveApiLogAsync(FacebookApiLog log);
}
