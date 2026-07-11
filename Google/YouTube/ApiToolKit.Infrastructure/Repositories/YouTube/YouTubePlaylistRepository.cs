using ApiToolKit.Domain.Entities.YouTube;
using ApiToolKit.Domain.Interfaces.Repositories.YouTube;
using ApiToolKit.Infrastructure.Data;
using Dapper;

namespace ApiToolKit.Infrastructure.Repositories.YouTube;

public class YouTubePlaylistRepository : IYouTubePlaylistRepository
{
    private readonly DapperContext _context;

    public YouTubePlaylistRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task SavePlaylistAsync(YouTubePlaylist playlist)
    {
        using var conn = _context.CreateConnection();
        await conn.ExecuteAsync(
            "sp_YouTube_SavePlaylist",
            new
            {
                playlist.PlaylistId,
                playlist.ChannelId,
                playlist.UserId,
                playlist.Title,
                playlist.Description,
                playlist.Status,
                playlist.ItemCount,
                playlist.PublishedAt,
                playlist.ThumbnailUrl
            },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task UpdatePlaylistAsync(YouTubePlaylist playlist)
    {
        using var conn = _context.CreateConnection();
        await conn.ExecuteAsync(
            "sp_YouTube_UpdatePlaylist",
            new
            {
                playlist.PlaylistId,
                playlist.Title,
                playlist.Description,
                playlist.Status,
                playlist.ItemCount
            },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task DeletePlaylistAsync(string playlistId)
    {
        using var conn = _context.CreateConnection();
        await conn.ExecuteAsync(
            "sp_YouTube_DeletePlaylist",
            new { PlaylistId = playlistId },
            commandType: System.Data.CommandType.StoredProcedure);
    }
}
