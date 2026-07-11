using ApiToolKit.Domain.Entities.GoogleAds;
using ApiToolKit.Domain.Interfaces.Repositories.GoogleAds;
using ApiToolKit.Infrastructure.Data;
using Dapper;
using System.Data;

namespace ApiToolKit.Infrastructure.Repositories.GoogleAds;

public class GoogleAdsCampaignRepository : IGoogleAdsCampaignRepository
{
    private readonly DapperContext _context;

    public GoogleAdsCampaignRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task SaveCampaignAsync(GoogleAdsCampaign campaign)
    {
        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(
            "sp_GoogleAds_SaveCampaign",
            new
            {
                campaign.ResourceName,
                campaign.CustomerId,
                campaign.Name,
                campaign.Status,
                campaign.AdvertisingChannelType,
                campaign.BiddingStrategyType,
                campaign.BudgetAmountMicros,
                campaign.StartDate,
                campaign.EndDate,
                CreatedAt = DateTime.UtcNow
            },
            commandType: CommandType.StoredProcedure);
    }

    public async Task UpdateCampaignAsync(GoogleAdsCampaign campaign)
    {
        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(
            "sp_GoogleAds_UpdateCampaign",
            new
            {
                campaign.ResourceName,
                campaign.CustomerId,
                campaign.Name,
                campaign.Status,
                campaign.BudgetAmountMicros,
                UpdatedAt = DateTime.UtcNow
            },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<GoogleAdsCampaign>> GetByCustomerIdAsync(string customerId)
    {
        using var connection = _context.CreateConnection();
        return await connection.QueryAsync<GoogleAdsCampaign>(
            "sp_GoogleAds_GetCampaignsByCustomer",
            new { CustomerId = customerId },
            commandType: CommandType.StoredProcedure);
    }
}
