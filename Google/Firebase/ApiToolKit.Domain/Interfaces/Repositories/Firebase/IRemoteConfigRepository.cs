using ApiToolKit.Domain.Entities.Firebase;

namespace ApiToolKit.Domain.Interfaces.Repositories.Firebase;

public interface IRemoteConfigRepository
{
    Task<int> SaveTemplateAsync(RemoteConfigTemplate template);
    Task<RemoteConfigTemplate?> GetLatestByProjectIdAsync(string projectId);
}
