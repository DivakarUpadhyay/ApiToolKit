using ApiToolKit.Domain.Entities.Cloud;

namespace ApiToolKit.Domain.Interfaces.Repositories.Cloud;

public interface ICloudAuthRepository
{
    Task<CloudConfig?> GetConfigAsync();
    Task<CloudToken?> GetTokenByProjectIdAsync(string projectId);
    Task SaveTokenAsync(CloudToken token);
    Task UpdateTokenAsync(CloudToken token);
    Task SaveApiLogAsync(CloudApiLog log);
}
