using ApiToolKit.Domain.Entities.Facebook;

namespace ApiToolKit.Domain.Interfaces.Repositories.Facebook;

public interface IFacebookThreadsRepository
{
    Task<int> SaveApiLogAsync(FacebookApiLog log);
}
