using ApiToolKit.Domain.Entities.Workspace;

namespace ApiToolKit.Domain.Interfaces.Repositories.Workspace;

public interface IWorkspaceAuthRepository
{
    Task<WorkspaceConfig?> GetConfigAsync();
    Task<WorkspaceToken?> GetTokenByUserIdAsync(string userId);
    Task SaveTokenAsync(WorkspaceToken token);
    Task UpdateTokenAsync(WorkspaceToken token);
    Task SaveApiLogAsync(WorkspaceApiLog log);
}
