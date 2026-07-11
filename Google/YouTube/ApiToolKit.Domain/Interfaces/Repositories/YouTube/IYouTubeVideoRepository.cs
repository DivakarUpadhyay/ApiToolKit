using ApiToolKit.Domain.Entities.YouTube;

namespace ApiToolKit.Domain.Interfaces.Repositories.YouTube;

public interface IYouTubeVideoRepository
{
    Task SaveVideoAsync(YouTubeVideo video);
    Task UpdateVideoAsync(YouTubeVideo video);
    Task DeleteVideoAsync(string videoId);
    Task<IEnumerable<YouTubeVideo>> GetByChannelIdAsync(string channelId);
}
