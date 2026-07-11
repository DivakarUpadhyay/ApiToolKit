using ApiToolKit.Domain.Entities.Facebook;
using ApiToolKit.Domain.Interfaces.Repositories.Facebook;
using ApiToolKit.Infrastructure.Data;
using Dapper;

namespace ApiToolKit.Infrastructure.Repositories.Facebook;

public class FacebookAdSetRepository : IFacebookAdSetRepository
{
    private readonly DapperContext _context;

    public FacebookAdSetRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<int> SaveAdSetAsync(FacebookAdSet adSet)
    {
        using var conn = _context.CreateConnection();
        return await conn.ExecuteAsync(
            "sp_Facebook_AdSet_Save",
            new
            {
                adSet.AdSetId,
                adSet.CampaignId,
                adSet.AccountId,
                AdSetName = adSet.Name,
                AdSetStatus = adSet.Status,
                adSet.DailyBudget,
                adSet.LifetimeBudget,
                adSet.StartTime,
                adSet.EndTime,
                adSet.BillingEvent,
                adSet.OptimizationGoal
            },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<FacebookAdSet?> GetAdSetByIdAsync(string adSetId)
    {
        using var conn = _context.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<FacebookAdSet>(
            "sp_Facebook_AdSet_GetById",
            new { AdSetId = adSetId },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<FacebookAdSet>> GetAdSetsByCampaignIdAsync(string campaignId, int limit, int offset)
    {
        using var conn = _context.CreateConnection();
        return await conn.QueryAsync<FacebookAdSet>(
            "sp_Facebook_AdSet_GetByCampaignId",
            new { CampaignId = campaignId, Limit = limit, Offset = offset },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<int> UpdateAdSetAsync(FacebookAdSet adSet)
    {
        using var conn = _context.CreateConnection();
        return await conn.ExecuteAsync(
            "sp_Facebook_AdSet_Update",
            new
            {
                adSet.AdSetId,
                AdSetName = adSet.Name,
                AdSetStatus = adSet.Status,
                adSet.DailyBudget,
                adSet.LifetimeBudget,
                adSet.StartTime,
                adSet.EndTime
            },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<int> DeleteAdSetAsync(string adSetId)
    {
        using var conn = _context.CreateConnection();
        return await conn.ExecuteAsync(
            "sp_Facebook_AdSet_Delete",
            new { AdSetId = adSetId },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<int> SaveApiLogAsync(FacebookApiLog log)
    {
        using var conn = _context.CreateConnection();
        return await conn.ExecuteAsync(
            "sp_Facebook_AdSet_SaveApiLog",
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
