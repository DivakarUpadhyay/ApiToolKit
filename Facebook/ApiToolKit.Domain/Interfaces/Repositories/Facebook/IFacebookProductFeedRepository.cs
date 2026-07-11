using ApiToolKit.Domain.Entities.Facebook;
namespace ApiToolKit.Domain.Interfaces.Repositories.Facebook;
public interface IFacebookProductFeedRepository
{
    Task<int> SaveApiLogAsync(FacebookApiLog log);
}
