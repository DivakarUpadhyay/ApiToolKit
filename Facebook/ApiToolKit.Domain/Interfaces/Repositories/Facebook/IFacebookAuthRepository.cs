using ApiToolKit.Domain.Entities.Facebook;

namespace ApiToolKit.Domain.Interfaces.Repositories.Facebook;

public interface IFacebookAuthRepository
{
    Task<FacebookConfig?> GetConfigAsync();
    Task<FacebookToken?> GetTokenByUserIdAsync(string userId);
    Task<int> SaveTokenAsync(FacebookToken token);
    Task<int> UpdateTokenAsync(FacebookToken token);
    Task<int> SaveApiLogAsync(FacebookApiLog log);
}
