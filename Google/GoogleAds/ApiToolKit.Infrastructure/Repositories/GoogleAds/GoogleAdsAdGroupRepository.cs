using ApiToolKit.Domain.Entities.GoogleAds;
using ApiToolKit.Domain.Interfaces.Repositories.GoogleAds;
using ApiToolKit.Infrastructure.Data;
using Dapper;
using System.Data;

namespace ApiToolKit.Infrastructure.Repositories.GoogleAds;

public class GoogleAdsAdGroupRepository : IGoogleAdsAdGroupRepository
{
    private readonly DapperContext _context;

    public GoogleAdsAdGroupRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task SaveAdGroupAsync(GoogleAdsAdGroup adGroup)
    {
        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(
            "sp_GoogleAds_SaveAdGroup",
            new
            {
                adGroup.ResourceName,
                adGroup.CustomerId,
                adGroup.CampaignResourceName,
                adGroup.Name,
                adGroup.Status,
                adGroup.Type,
                adGroup.CpcBidMicros,
                CreatedAt = DateTime.UtcNow
            },
            commandType: CommandType.StoredProcedure);
    }

    public async Task UpdateAdGroupAsync(GoogleAdsAdGroup adGroup)
    {
        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(
            "sp_GoogleAds_UpdateAdGroup",
            new
            {
                adGroup.ResourceName,
                adGroup.CustomerId,
                adGroup.Name,
                adGroup.Status,
                adGroup.CpcBidMicros,
                UpdatedAt = DateTime.UtcNow
            },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<GoogleAdsAdGroup>> GetByCampaignAsync(string customerId, string campaignResourceName)
    {
        using var connection = _context.CreateConnection();
        return await connection.QueryAsync<GoogleAdsAdGroup>(
            "sp_GoogleAds_GetAdGroupsByCampaign",
            new { CustomerId = customerId, CampaignResourceName = campaignResourceName },
            commandType: CommandType.StoredProcedure);
    }
}
