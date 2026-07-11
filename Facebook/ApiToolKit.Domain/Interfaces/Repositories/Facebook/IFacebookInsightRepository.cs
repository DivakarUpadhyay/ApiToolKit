using ApiToolKit.Domain.Entities.Facebook;

namespace ApiToolKit.Domain.Interfaces.Repositories.Facebook;

public interface IFacebookInsightRepository
{
    Task<int> SavePageInsightAsync(string pageId, string metric, string period, string value, DateTime endTime);
    Task<int> SaveAdInsightAsync(string accountId, string? campaignId, string? adSetId, string? adId, string data, DateTime dateStart, DateTime dateStop);
    Task<IEnumerable<dynamic>> GetPageInsightsAsync(string pageId, string metric, DateTime since, DateTime until);
    Task<IEnumerable<dynamic>> GetAdInsightsAsync(string accountId, DateTime since, DateTime until);
    Task<int> SaveApiLogAsync(FacebookApiLog log);
}
