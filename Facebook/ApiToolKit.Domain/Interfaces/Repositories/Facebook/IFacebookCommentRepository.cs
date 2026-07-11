using ApiToolKit.Domain.Entities.Facebook;

namespace ApiToolKit.Domain.Interfaces.Repositories.Facebook;

public interface IFacebookCommentRepository
{
    Task<int> SaveCommentAsync(FacebookComment comment);
    Task<FacebookComment?> GetCommentByIdAsync(string commentId);
    Task<IEnumerable<FacebookComment>> GetCommentsByObjectIdAsync(string objectId, int limit, int offset);
    Task<int> DeleteCommentAsync(string commentId);
    Task<int> SaveApiLogAsync(FacebookApiLog log);
}
