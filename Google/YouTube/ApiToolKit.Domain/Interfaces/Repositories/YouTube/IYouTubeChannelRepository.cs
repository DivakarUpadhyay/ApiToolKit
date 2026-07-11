using ApiToolKit.Domain.Entities.YouTube;

namespace ApiToolKit.Domain.Interfaces.Repositories.YouTube;

public interface IYouTubeChannelRepository
{
    Task SaveChannelAsync(YouTubeChannel channel);
    Task UpdateChannelAsync(YouTubeChannel channel);
    Task<YouTubeChannel?> GetByUserIdAsync(string userId);
}
