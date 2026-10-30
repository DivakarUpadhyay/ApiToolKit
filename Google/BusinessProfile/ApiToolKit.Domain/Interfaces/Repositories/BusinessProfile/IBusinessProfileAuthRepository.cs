using ApiToolKit.Domain.Entities.BusinessProfile;

namespace ApiToolKit.Domain.Interfaces.Repositories.BusinessProfile;

public interface IBusinessProfileAuthRepository
{
    Task<BusinessProfileToken?> GetTokenAsync(string accountKey);
    Task SaveTokenAsync(BusinessProfileToken token);
    Task SaveApiLogAsync(BusinessProfileApiLog log);
}
