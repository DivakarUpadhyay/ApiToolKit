using ApiToolKit.Domain.Entities.Analytics;

namespace ApiToolKit.Domain.Interfaces.Repositories.Analytics;

public interface IAnalyticsReportRepository
{
    Task SaveReportAsync(AnalyticsSavedReport report);
    Task<IEnumerable<AnalyticsSavedReport>> GetReportsByPropertyIdAsync(string propertyId);
}
