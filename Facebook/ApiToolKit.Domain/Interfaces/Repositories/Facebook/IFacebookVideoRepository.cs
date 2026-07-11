using ApiToolKit.Domain.Entities.Facebook;

namespace ApiToolKit.Domain.Interfaces.Repositories.Facebook;

public interface IFacebookVideoRepository
{
    Task<int> SaveVideoAsync(FacebookVideo video);
    Task<FacebookVideo?> GetVideoByIdAsync(string videoId);
    Task<IEnumerable<FacebookVideo>> GetVideosByPageIdAsync(string pageId, int limit, int offset);
    Task<int> UpdateVideoAsync(FacebookVideo video);
    Task<int> DeleteVideoAsync(string videoId);
    Task<int> SaveApiLogAsync(FacebookApiLog log);
}
