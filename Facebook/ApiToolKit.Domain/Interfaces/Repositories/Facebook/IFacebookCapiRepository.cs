using ApiToolKit.Domain.Entities.Facebook;
namespace ApiToolKit.Domain.Interfaces.Repositories.Facebook;
public interface IFacebookCapiRepository { Task<int> SaveApiLogAsync(FacebookApiLog log); }
