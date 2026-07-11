using ApiToolKit.Domain.Entities.Facebook;
namespace ApiToolKit.Domain.Interfaces.Repositories.Facebook;
public interface IFacebookLiveRepository { Task<int> SaveApiLogAsync(FacebookApiLog log); }
