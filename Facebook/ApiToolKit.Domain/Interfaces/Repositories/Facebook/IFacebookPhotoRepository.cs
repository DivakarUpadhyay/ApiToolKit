using ApiToolKit.Domain.Entities.Facebook;

namespace ApiToolKit.Domain.Interfaces.Repositories.Facebook;

public interface IFacebookPhotoRepository
{
    Task<int> SavePhotoAsync(FacebookPhoto photo);
    Task<FacebookPhoto?> GetPhotoByIdAsync(string photoId);
    Task<IEnumerable<FacebookPhoto>> GetPhotosByPageIdAsync(string pageId, int limit, int offset);
    Task<int> DeletePhotoAsync(string photoId);
    Task<int> SaveApiLogAsync(FacebookApiLog log);
}
