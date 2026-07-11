using ApiToolKit.Domain.Entities.Facebook;

namespace ApiToolKit.Domain.Interfaces.Repositories.Facebook;

public interface IFacebookWhatsAppRepository
{
    Task<int> SaveApiLogAsync(FacebookApiLog log);
}
