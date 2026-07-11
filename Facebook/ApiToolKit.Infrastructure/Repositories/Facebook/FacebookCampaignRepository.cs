using ApiToolKit.Domain.Entities.Facebook;
using ApiToolKit.Domain.Interfaces.Repositories.Facebook;
using ApiToolKit.Infrastructure.Data;
using Dapper;

namespace ApiToolKit.Infrastructure.Repositories.Facebook;

public class FacebookCampaignRepository : IFacebookCampaignRepository
{
    private readonly DapperContext _context;

    public FacebookCampaignRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<int> SaveCampaignAsync(FacebookCampaign campaign)
    {
        using var conn = _context.CreateConnection();
        return await conn.ExecuteAsync(
            "sp_Facebook_Campaign_Save",
            new
            {
                campaign.CampaignId,
                campaign.AccountId,
                CampaignName = campaign.Name,
                campaign.Objective,
                CampaignStatus = campaign.Status,
                campaign.DailyBudget,
                campaign.LifetimeBudget,
                campaign.BudgetRemaining
            },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<FacebookCampaign?> GetCampaignByIdAsync(string campaignId)
    {
        using var conn = _context.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<FacebookCampaign>(
            "sp_Facebook_Campaign_GetById",
            new { CampaignId = campaignId },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<FacebookCampaign>> GetCampaignsByAccountIdAsync(string accountId, int limit, int offset)
    {
        using var conn = _context.CreateConnection();
        return await conn.QueryAsync<FacebookCampaign>(
            "sp_Facebook_Campaign_GetByAccountId",
            new { AccountId = accountId, Limit = limit, Offset = offset },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<int> UpdateCampaignAsync(FacebookCampaign campaign)
    {
        using var conn = _context.CreateConnection();
        return await conn.ExecuteAsync(
            "sp_Facebook_Campaign_Update",
            new
            {
                campaign.CampaignId,
                CampaignName = campaign.Name,
                CampaignStatus = campaign.Status,
                campaign.DailyBudget,
                campaign.LifetimeBudget
            },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<int> DeleteCampaignAsync(string campaignId)
    {
        using var conn = _context.CreateConnection();
        return await conn.ExecuteAsync(
            "sp_Facebook_Campaign_Delete",
            new { CampaignId = campaignId },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<int> SaveApiLogAsync(FacebookApiLog log)
    {
        using var conn = _context.CreateConnection();
        return await conn.ExecuteAsync(
            "sp_Facebook_Campaign_SaveApiLog",
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
