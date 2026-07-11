using ApiToolKit.Domain.Entities.Facebook;
using ApiToolKit.Domain.Interfaces.Repositories.Facebook;
using ApiToolKit.Infrastructure.Data;
using Dapper;

namespace ApiToolKit.Infrastructure.Repositories.Facebook;

public class FacebookPostRepository : IFacebookPostRepository
{
    private readonly DapperContext _context;

    public FacebookPostRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<int> SavePostAsync(FacebookPost post)
    {
        using var conn = _context.CreateConnection();
        return await conn.ExecuteAsync(
            "sp_Facebook_Post_Save",
            new
            {
                post.PostId,
                post.PageId,
                post.UserId,
                post.Message,
                post.Story,
                post.FullPicture,
                post.PermalinkUrl,
                post.IsPublished,
                ScheduledTime = (DateTime?)null
            },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<FacebookPost?> GetPostByIdAsync(string postId)
    {
        using var conn = _context.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<FacebookPost>(
            "sp_Facebook_Post_GetById",
            new { PostId = postId },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<FacebookPost>> GetPostsByPageIdAsync(string pageId, int limit, int offset)
    {
        using var conn = _context.CreateConnection();
        return await conn.QueryAsync<FacebookPost>(
            "sp_Facebook_Post_GetByPageId",
            new { PageId = pageId, Limit = limit, Offset = offset },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<int> UpdatePostAsync(FacebookPost post)
    {
        using var conn = _context.CreateConnection();
        return await conn.ExecuteAsync(
            "sp_Facebook_Post_Update",
            new
            {
                post.PostId,
                post.Message,
                post.IsPublished,
                ScheduledTime = (DateTime?)null
            },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<int> DeletePostAsync(string postId)
    {
        using var conn = _context.CreateConnection();
        return await conn.ExecuteAsync(
            "sp_Facebook_Post_Delete",
            new { PostId = postId },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<int> SaveApiLogAsync(FacebookApiLog log)
    {
        using var conn = _context.CreateConnection();
        return await conn.ExecuteAsync(
            "sp_Facebook_Post_SaveApiLog",
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
