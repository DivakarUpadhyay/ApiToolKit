using ApiToolKit.Domain.Entities.GoogleAds;
using ApiToolKit.Domain.Interfaces.Repositories.GoogleAds;
using ApiToolKit.Infrastructure.Data;
using Dapper;
using System.Data;

namespace ApiToolKit.Infrastructure.Repositories.GoogleAds;

public class GoogleAdsKeywordRepository : IGoogleAdsKeywordRepository
{
    private readonly DapperContext _context;

    public GoogleAdsKeywordRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task SaveKeywordAsync(GoogleAdsKeyword keyword)
    {
        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(
            "sp_GoogleAds_SaveKeyword",
            new
            {
                keyword.ResourceName,
                keyword.CustomerId,
                keyword.AdGroupResourceName,
                keyword.Text,
                keyword.MatchType,
                keyword.Status,
                keyword.CpcBidMicros,
                CreatedAt = DateTime.UtcNow
            },
            commandType: CommandType.StoredProcedure);
    }

    public async Task UpdateKeywordAsync(GoogleAdsKeyword keyword)
    {
        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(
            "sp_GoogleAds_UpdateKeyword",
            new
            {
                keyword.ResourceName,
                keyword.CustomerId,
                keyword.Status,
                keyword.CpcBidMicros,
                UpdatedAt = DateTime.UtcNow
            },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<GoogleAdsKeyword>> GetByAdGroupAsync(string customerId, string adGroupResourceName)
    {
        using var connection = _context.CreateConnection();
        return await connection.QueryAsync<GoogleAdsKeyword>(
            "sp_GoogleAds_GetKeywordsByAdGroup",
            new { CustomerId = customerId, AdGroupResourceName = adGroupResourceName },
            commandType: CommandType.StoredProcedure);
    }
}
