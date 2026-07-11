using ApiToolKit.Domain.Entities.Facebook;
namespace ApiToolKit.Domain.Interfaces.Repositories.Facebook;
public interface IFacebookInstagramShoppingRepository { Task<int> SaveApiLogAsync(FacebookApiLog log); }
