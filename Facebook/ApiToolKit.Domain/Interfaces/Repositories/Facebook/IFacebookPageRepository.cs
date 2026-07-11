using ApiToolKit.Domain.Entities.Facebook;

namespace ApiToolKit.Domain.Interfaces.Repositories.Facebook;

public interface IFacebookPageRepository
{
    Task<int> SavePageAsync(FacebookPage page);
    Task<FacebookPage?> GetPageByIdAsync(string pageId);
    Task<IEnumerable<FacebookPage>> GetPagesByUserIdAsync(string userId);
    Task<int> UpdatePageAsync(FacebookPage page);
    Task<int> SaveApiLogAsync(FacebookApiLog log);
}
