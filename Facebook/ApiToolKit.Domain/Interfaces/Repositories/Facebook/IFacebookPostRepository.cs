using ApiToolKit.Domain.Entities.Facebook;

namespace ApiToolKit.Domain.Interfaces.Repositories.Facebook;

public interface IFacebookPostRepository
{
    Task<int> SavePostAsync(FacebookPost post);
    Task<FacebookPost?> GetPostByIdAsync(string postId);
    Task<IEnumerable<FacebookPost>> GetPostsByPageIdAsync(string pageId, int limit, int offset);
    Task<int> UpdatePostAsync(FacebookPost post);
    Task<int> DeletePostAsync(string postId);
    Task<int> SaveApiLogAsync(FacebookApiLog log);
}
