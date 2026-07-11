using ApiToolKit.Domain.Entities.YouTube;
using ApiToolKit.Domain.Interfaces.Repositories.YouTube;
using ApiToolKit.Infrastructure.Data;
using Dapper;

namespace ApiToolKit.Infrastructure.Repositories.YouTube;

public class YouTubeVideoRepository : IYouTubeVideoRepository
{
    private readonly DapperContext _context;

    public YouTubeVideoRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task SaveVideoAsync(YouTubeVideo video)
    {
        using var conn = _context.CreateConnection();
        await conn.ExecuteAsync(
            "sp_YouTube_SaveVideo",
            new
            {
                video.VideoId,
                video.ChannelId,
                video.UserId,
                video.Title,
                video.Description,
                video.Status,
                video.PublishedAt,
                video.ViewCount,
                video.LikeCount,
                video.CommentCount,
                video.Duration,
                video.ThumbnailUrl
            },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task UpdateVideoAsync(YouTubeVideo video)
    {
        using var conn = _context.CreateConnection();
        await conn.ExecuteAsync(
            "sp_YouTube_UpdateVideo",
            new
            {
                video.VideoId,
                video.Title,
                video.Description,
                video.Status,
                video.ViewCount,
                video.LikeCount,
                video.CommentCount
            },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task DeleteVideoAsync(string videoId)
    {
        using var conn = _context.CreateConnection();
        await conn.ExecuteAsync(
            "sp_YouTube_DeleteVideo",
            new { VideoId = videoId },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<YouTubeVideo>> GetByChannelIdAsync(string channelId)
    {
        using var conn = _context.CreateConnection();
        return await conn.QueryAsync<YouTubeVideo>(
            "sp_YouTube_GetVideosByChannelId",
            new { ChannelId = channelId },
            commandType: System.Data.CommandType.StoredProcedure);
    }
}
