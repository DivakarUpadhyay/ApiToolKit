using ApiToolKit.Domain.Entities.YouTube;
using ApiToolKit.Domain.Interfaces.Repositories.YouTube;
using ApiToolKit.Infrastructure.Data;
using Dapper;

namespace ApiToolKit.Infrastructure.Repositories.YouTube;

public class YouTubeChannelRepository : IYouTubeChannelRepository
{
    private readonly DapperContext _context;

    public YouTubeChannelRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task SaveChannelAsync(YouTubeChannel channel)
    {
        using var conn = _context.CreateConnection();
        await conn.ExecuteAsync(
            "sp_YouTube_SaveChannel",
            new
            {
                channel.ChannelId,
                channel.UserId,
                channel.Title,
                channel.Description,
                channel.CustomUrl,
                channel.PublishedAt,
                channel.Country,
                channel.ViewCount,
                channel.SubscriberCount,
                channel.VideoCount,
                channel.ThumbnailUrl
            },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task UpdateChannelAsync(YouTubeChannel channel)
    {
        using var conn = _context.CreateConnection();
        await conn.ExecuteAsync(
            "sp_YouTube_UpdateChannel",
            new
            {
                channel.ChannelId,
                channel.Title,
                channel.Description,
                channel.CustomUrl,
                channel.Country,
                channel.ViewCount,
                channel.SubscriberCount,
                channel.VideoCount,
                channel.ThumbnailUrl
            },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<YouTubeChannel?> GetByUserIdAsync(string userId)
    {
        using var conn = _context.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<YouTubeChannel>(
            "sp_YouTube_GetChannelByUserId",
            new { UserId = userId },
            commandType: System.Data.CommandType.StoredProcedure);
    }
}
