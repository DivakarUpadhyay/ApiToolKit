using ApiToolKit.Domain.Entities.Facebook;
namespace ApiToolKit.Domain.Interfaces.Repositories.Facebook;
public interface IFacebookBusinessManagerRepository { Task<int> SaveApiLogAsync(FacebookApiLog log); }
