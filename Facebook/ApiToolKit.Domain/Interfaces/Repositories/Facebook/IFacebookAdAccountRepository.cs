using ApiToolKit.Domain.Entities.Facebook;

namespace ApiToolKit.Domain.Interfaces.Repositories.Facebook;

public interface IFacebookAdAccountRepository
{
    Task<int> SaveAdAccountAsync(FacebookAdAccount adAccount);
    Task<FacebookAdAccount?> GetAdAccountByIdAsync(string accountId);
    Task<IEnumerable<FacebookAdAccount>> GetAdAccountsByUserIdAsync(string userId);
    Task<int> SaveApiLogAsync(FacebookApiLog log);
}
