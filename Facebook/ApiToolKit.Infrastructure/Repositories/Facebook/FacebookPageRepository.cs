using ApiToolKit.Domain.Entities.Facebook;
using ApiToolKit.Domain.Interfaces.Repositories.Facebook;
using ApiToolKit.Infrastructure.Data;
using Dapper;

namespace ApiToolKit.Infrastructure.Repositories.Facebook;

public class FacebookPageRepository : IFacebookPageRepository
{
    private readonly DapperContext _context;

    public FacebookPageRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<int> SavePageAsync(FacebookPage page)
    {
        using var conn = _context.CreateConnection();
        return await conn.ExecuteAsync(
            "sp_Facebook_Page_Save",
            new
            {
                page.PageId,
                page.PageName,
                page.PageCategory,
                page.AccessToken,
                page.UserId,
                FollowersCount = page.Followers,
                LikesCount = page.Likes,
                page.IsPublished
            },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<FacebookPage?> GetPageByIdAsync(string pageId)
    {
        using var conn = _context.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<FacebookPage>(
            "sp_Facebook_Page_GetById",
            new { PageId = pageId },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<FacebookPage>> GetPagesByUserIdAsync(string userId)
    {
        using var conn = _context.CreateConnection();
        return await conn.QueryAsync<FacebookPage>(
            "sp_Facebook_Page_GetByUserId",
            new { UserId = userId },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<int> UpdatePageAsync(FacebookPage page)
    {
        using var conn = _context.CreateConnection();
        return await conn.ExecuteAsync(
            "sp_Facebook_Page_Update",
            new
            {
                page.PageId,
                page.PageName,
                page.PageCategory,
                page.AccessToken,
                FollowersCount = page.Followers,
                LikesCount = page.Likes,
                page.IsPublished
            },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<int> SaveApiLogAsync(FacebookApiLog log)
    {
        using var conn = _context.CreateConnection();
        return await conn.ExecuteAsync(
            "sp_Facebook_Page_SaveApiLog",
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
