using ApiToolKit.Domain.Entities.Merchant;

namespace ApiToolKit.Domain.Interfaces.Repositories.Merchant;

public interface IMerchantAuthRepository
{
    Task<MerchantToken?> GetTokenAsync(string accountKey);
    Task SaveTokenAsync(MerchantToken token);
    Task SaveApiLogAsync(MerchantApiLog log);
}
