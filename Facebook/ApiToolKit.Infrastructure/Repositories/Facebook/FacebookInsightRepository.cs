using ApiToolKit.Domain.Entities.Facebook;
using ApiToolKit.Domain.Interfaces.Repositories.Facebook;
using ApiToolKit.Infrastructure.Data;
using Dapper;

namespace ApiToolKit.Infrastructure.Repositories.Facebook;

public class FacebookInsightRepository : IFacebookInsightRepository
{
    private readonly DapperContext _context;

    public FacebookInsightRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<int> SavePageInsightAsync(string pageId, string metric, string period, string value, DateTime endTime)
    {
        using var conn = _context.CreateConnection();
        return await conn.ExecuteAsync(
            "sp_Facebook_Insight_SavePage",
            new { PageId = pageId, MetricName = metric, Period = period, MetricValue = value, EndTime = endTime },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<int> SaveAdInsightAsync(string accountId, string? campaignId, string? adSetId, string? adId, string data, DateTime dateStart, DateTime dateStop)
    {
        using var conn = _context.CreateConnection();
        return await conn.ExecuteAsync(
            "sp_Facebook_Insight_SaveAd",
            new { AccountId = accountId, CampaignId = campaignId, AdSetId = adSetId, AdId = adId, Data = data, DateStart = dateStart, DateStop = dateStop },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<dynamic>> GetPageInsightsAsync(string pageId, string metric, DateTime since, DateTime until)
    {
        using var conn = _context.CreateConnection();
        return await conn.QueryAsync(
            "sp_Facebook_Insight_GetPage",
            new { PageId = pageId, Metric = metric, Since = since, Until = until },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<dynamic>> GetAdInsightsAsync(string accountId, DateTime since, DateTime until)
    {
        using var conn = _context.CreateConnection();
        return await conn.QueryAsync(
            "sp_Facebook_Insight_GetAd",
            new { AccountId = accountId, Since = since, Until = until },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<int> SaveApiLogAsync(FacebookApiLog log)
    {
        using var conn = _context.CreateConnection();
        return await conn.ExecuteAsync(
            "sp_Facebook_Insight_SaveApiLog",
            new
            {
                log.UserId,
                log.PageId,
                log.ApiName,
                log.RequestBody,
                log.ResponseBody,
                log.IsSuccess,
                log.StatusCode
            },
            commandType: System.Data.CommandType.StoredProcedure);
    }
}
