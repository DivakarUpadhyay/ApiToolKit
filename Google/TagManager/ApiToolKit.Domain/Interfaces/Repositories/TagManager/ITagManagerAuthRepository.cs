using ApiToolKit.Domain.Entities.TagManager;

namespace ApiToolKit.Domain.Interfaces.Repositories.TagManager;

public interface ITagManagerAuthRepository
{
    Task<TagManagerToken?> GetTokenAsync(string accountKey);
    Task SaveTokenAsync(TagManagerToken token);
    Task SaveApiLogAsync(TagManagerApiLog log);
}
