using ApiToolKit.Domain.Entities.Analytics;
using ApiToolKit.Domain.Interfaces.Repositories.Analytics;
using ApiToolKit.Infrastructure.Data;
using Dapper;

namespace ApiToolKit.Infrastructure.Repositories.Analytics;

public class AnalyticsReportRepository : IAnalyticsReportRepository
{
    private readonly DapperContext _context;

    public AnalyticsReportRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task SaveReportAsync(AnalyticsSavedReport report)
    {
        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(
            "sp_Analytics_SaveReport",
            new
            {
                report.PropertyId,
                report.ReportName,
                report.Query,
                report.ResponseJson,
                report.RowCount
            },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<AnalyticsSavedReport>> GetReportsByPropertyIdAsync(string propertyId)
    {
        using var connection = _context.CreateConnection();
        return await connection.QueryAsync<AnalyticsSavedReport>(
            "sp_Analytics_GetReportsByPropertyId",
            new { PropertyId = propertyId },
            commandType: System.Data.CommandType.StoredProcedure);
    }
}
