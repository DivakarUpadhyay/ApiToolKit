using ApiToolKit.Domain.Entities.Facebook;
using ApiToolKit.Domain.Interfaces.Repositories.Facebook;
using ApiToolKit.Infrastructure.Data;
using Dapper;

namespace ApiToolKit.Infrastructure.Repositories.Facebook;

public class FacebookAdRepository : IFacebookAdRepository
{
    private readonly DapperContext _context;

    public FacebookAdRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<int> SaveAdAsync(FacebookAd ad)
    {
        using var conn = _context.CreateConnection();
        return await conn.ExecuteAsync(
            "sp_Facebook_Ad_Save",
            new
            {
                ad.AdId,
                ad.AdSetId,
                ad.CampaignId,
                ad.AccountId,
                AdName = ad.Name,
                AdStatus = ad.Status,
                ad.CreativeId
            },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<FacebookAd?> GetAdByIdAsync(string adId)
    {
        using var conn = _context.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<FacebookAd>(
            "sp_Facebook_Ad_GetById",
            new { AdId = adId },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<FacebookAd>> GetAdsByAdSetIdAsync(string adSetId, int limit, int offset)
    {
        using var conn = _context.CreateConnection();
        return await conn.QueryAsync<FacebookAd>(
            "sp_Facebook_Ad_GetByAdSetId",
            new { AdSetId = adSetId, Limit = limit, Offset = offset },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<int> UpdateAdAsync(FacebookAd ad)
    {
        using var conn = _context.CreateConnection();
        return await conn.ExecuteAsync(
            "sp_Facebook_Ad_Update",
            new
            {
                ad.AdId,
                AdName = ad.Name,
                AdStatus = ad.Status
            },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<int> DeleteAdAsync(string adId)
    {
        using var conn = _context.CreateConnection();
        return await conn.ExecuteAsync(
            "sp_Facebook_Ad_Delete",
            new { AdId = adId },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<int> SaveApiLogAsync(FacebookApiLog log)
    {
        using var conn = _context.CreateConnection();
        return await conn.ExecuteAsync(
            "sp_Facebook_Ad_SaveApiLog",
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
