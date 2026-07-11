using ApiToolKit.Domain.Entities.Facebook;
using ApiToolKit.Domain.Interfaces.Repositories.Facebook;
using ApiToolKit.Infrastructure.Data;
using Dapper;

namespace ApiToolKit.Infrastructure.Repositories.Facebook;

public class FacebookVideoRepository : IFacebookVideoRepository
{
    private readonly DapperContext _context;

    public FacebookVideoRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<int> SaveVideoAsync(FacebookVideo video)
    {
        using var conn = _context.CreateConnection();
        return await conn.ExecuteAsync(
            "sp_Facebook_Video_Save",
            new
            {
                video.VideoId,
                video.PageId,
                video.UserId,
                video.Title,
                video.Description,
                video.Source,
                video.EmbedHtml,
                VideoStatus = video.Status,
                video.Length
            },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<FacebookVideo?> GetVideoByIdAsync(string videoId)
    {
        using var conn = _context.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<FacebookVideo>(
            "sp_Facebook_Video_GetById",
            new { VideoId = videoId },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<FacebookVideo>> GetVideosByPageIdAsync(string pageId, int limit, int offset)
    {
        using var conn = _context.CreateConnection();
        return await conn.QueryAsync<FacebookVideo>(
            "sp_Facebook_Video_GetByPageId",
            new { PageId = pageId, Limit = limit, Offset = offset },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<int> UpdateVideoAsync(FacebookVideo video)
    {
        using var conn = _context.CreateConnection();
        return await conn.ExecuteAsync(
            "sp_Facebook_Video_Update",
            new
            {
                video.VideoId,
                video.Title,
                video.Description,
                VideoStatus = video.Status
            },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<int> DeleteVideoAsync(string videoId)
    {
        using var conn = _context.CreateConnection();
        return await conn.ExecuteAsync(
            "sp_Facebook_Video_Delete",
            new { VideoId = videoId },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<int> SaveApiLogAsync(FacebookApiLog log)
    {
        using var conn = _context.CreateConnection();
        return await conn.ExecuteAsync(
            "sp_Facebook_Video_SaveApiLog",
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
