using ApiToolKit.Domain.Entities.DisplayVideo;

namespace ApiToolKit.Domain.Interfaces.Repositories.DisplayVideo;

public interface IDisplayVideoAuthRepository
{
    Task<DisplayVideoToken?> GetTokenAsync(string accountKey);
    Task SaveTokenAsync(DisplayVideoToken token);
    Task SaveApiLogAsync(DisplayVideoApiLog log);
}
