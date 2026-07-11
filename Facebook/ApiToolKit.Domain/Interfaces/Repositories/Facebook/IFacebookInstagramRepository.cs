using ApiToolKit.Domain.Entities.Facebook;

namespace ApiToolKit.Domain.Interfaces.Repositories.Facebook;

public interface IFacebookInstagramRepository
{
    Task<int> SaveInstagramAccountAsync(FacebookInstagram account);
    Task<FacebookInstagram?> GetInstagramByPageIdAsync(string pageId);
    Task<int> UpdateInstagramAsync(FacebookInstagram account);
    Task<int> SaveApiLogAsync(FacebookApiLog log);
}
