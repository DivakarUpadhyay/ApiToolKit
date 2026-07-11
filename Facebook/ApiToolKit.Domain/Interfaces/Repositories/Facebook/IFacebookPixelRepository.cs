using ApiToolKit.Domain.Entities.Facebook;
namespace ApiToolKit.Domain.Interfaces.Repositories.Facebook;
public interface IFacebookPixelRepository { Task<int> SaveApiLogAsync(FacebookApiLog log); }
