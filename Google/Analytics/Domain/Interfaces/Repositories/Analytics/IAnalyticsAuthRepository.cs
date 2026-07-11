using ApiToolKit.Domain.Entities.Analytics;

namespace ApiToolKit.Domain.Interfaces.Repositories.Analytics;

public interface IAnalyticsAuthRepository
{
    Task<AnalyticsConfig?> GetConfigAsync();
    Task<AnalyticsToken?> GetTokenByPropertyIdAsync(string propertyId);
    Task SaveTokenAsync(AnalyticsToken token);
    Task UpdateTokenAsync(AnalyticsToken token);
    Task SaveApiLogAsync(AnalyticsApiLog log);
}
