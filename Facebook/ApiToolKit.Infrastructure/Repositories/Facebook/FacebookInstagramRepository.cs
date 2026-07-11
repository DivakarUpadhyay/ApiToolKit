using ApiToolKit.Domain.Entities.Facebook;
using ApiToolKit.Domain.Interfaces.Repositories.Facebook;
using ApiToolKit.Infrastructure.Data;
using Dapper;

namespace ApiToolKit.Infrastructure.Repositories.Facebook;

public class FacebookInstagramRepository : IFacebookInstagramRepository
{
    private readonly DapperContext _context;

    public FacebookInstagramRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<int> SaveInstagramAccountAsync(FacebookInstagram account)
    {
        using var conn = _context.CreateConnection();
        return await conn.ExecuteAsync(
            "sp_Facebook_Instagram_Save",
            new
            {
                account.IgUserId,
                account.PageId,
                account.Username,
                IgName = account.Name,
                account.Biography,
                FollowersCount = account.Followers,
                account.FollowsCount,
                account.MediaCount,
                account.ProfilePictureUrl,
                account.Website
            },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<FacebookInstagram?> GetInstagramByPageIdAsync(string pageId)
    {
        using var conn = _context.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<FacebookInstagram>(
            "sp_Facebook_Instagram_GetByPageId",
            new { PageId = pageId },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<int> UpdateInstagramAsync(FacebookInstagram account)
    {
        using var conn = _context.CreateConnection();
        return await conn.ExecuteAsync(
            "sp_Facebook_Instagram_Update",
            new
            {
                account.IgUserId,
                account.Username,
                IgName = account.Name,
                account.Biography,
                FollowersCount = account.Followers,
                account.FollowsCount,
                account.MediaCount,
                account.ProfilePictureUrl,
                account.Website
            },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<int> SaveApiLogAsync(FacebookApiLog log)
    {
        using var conn = _context.CreateConnection();
        return await conn.ExecuteAsync(
            "sp_Facebook_Instagram_SaveApiLog",
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
