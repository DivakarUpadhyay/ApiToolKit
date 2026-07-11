using ApiToolKit.Domain.Entities.Facebook;

namespace ApiToolKit.Domain.Interfaces.Repositories.Facebook;

public interface IFacebookUserRepository
{
    Task<int> SaveUserAsync(string userId, string name, string? email, string? picture);
    Task<dynamic?> GetUserByIdAsync(string userId);
    Task<int> SaveApiLogAsync(FacebookApiLog log);
}
