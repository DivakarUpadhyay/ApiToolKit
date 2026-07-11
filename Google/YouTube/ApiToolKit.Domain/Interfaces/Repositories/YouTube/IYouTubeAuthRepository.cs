using ApiToolKit.Domain.Entities.YouTube;

namespace ApiToolKit.Domain.Interfaces.Repositories.YouTube;

public interface IYouTubeAuthRepository
{
    Task<YouTubeConfig?> GetConfigAsync();
    Task<YouTubeToken?> GetTokenByUserIdAsync(string userId);
    Task SaveTokenAsync(YouTubeToken token);
    Task UpdateTokenAsync(YouTubeToken token);
    Task SaveApiLogAsync(YouTubeApiLog log);
}
