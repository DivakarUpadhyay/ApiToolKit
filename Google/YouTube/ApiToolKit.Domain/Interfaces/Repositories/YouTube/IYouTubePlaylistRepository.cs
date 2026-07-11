using ApiToolKit.Domain.Entities.YouTube;

namespace ApiToolKit.Domain.Interfaces.Repositories.YouTube;

public interface IYouTubePlaylistRepository
{
    Task SavePlaylistAsync(YouTubePlaylist playlist);
    Task UpdatePlaylistAsync(YouTubePlaylist playlist);
    Task DeletePlaylistAsync(string playlistId);
}
