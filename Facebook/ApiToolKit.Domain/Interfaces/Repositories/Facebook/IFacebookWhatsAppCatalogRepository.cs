using ApiToolKit.Domain.Entities.Facebook;
namespace ApiToolKit.Domain.Interfaces.Repositories.Facebook;
public interface IFacebookWhatsAppCatalogRepository { Task<int> SaveApiLogAsync(FacebookApiLog log); }
