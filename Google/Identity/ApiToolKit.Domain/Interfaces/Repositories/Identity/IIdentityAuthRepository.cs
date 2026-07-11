using ApiToolKit.Domain.Entities.Identity;

namespace ApiToolKit.Domain.Interfaces.Repositories.Identity;

public interface IIdentityAuthRepository
{
    Task<IdentityConfig?> GetConfigAsync();
    Task<IdentityToken?> GetTokenByUserIdAsync(string userId);
    Task<int> SaveTokenAsync(IdentityToken token);
    Task<int> UpdateTokenAsync(IdentityToken token);
    Task<int> SaveApiLogAsync(IdentityApiLog log);
}
