using ApiToolKit.Domain.Entities.Facebook;
namespace ApiToolKit.Domain.Interfaces.Repositories.Facebook;
public interface IFacebookCommerceRepository
{
    Task<int> SaveApiLogAsync(FacebookApiLog log);
}
