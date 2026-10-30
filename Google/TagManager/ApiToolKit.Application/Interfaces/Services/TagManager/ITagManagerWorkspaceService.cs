using ApiToolKit.Application.DTOs.TagManager.Workspace;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.TagManager;

public interface ITagManagerWorkspaceService
{
    Task<Result<object>> ListWorkspacesAsync(ListWorkspacesRequest request);
    Task<Result<object>> GetWorkspaceAsync(GetWorkspaceRequest request);
    Task<Result<object>> CreateWorkspaceAsync(CreateWorkspaceRequest request);
    Task<Result<object>> DeleteWorkspaceAsync(DeleteWorkspaceRequest request);
    Task<Result<object>> GetWorkspaceStatusAsync(GetWorkspaceStatusRequest request);
    Task<Result<object>> CreateVersionAsync(CreateVersionRequest request);
    Task<Result<object>> SyncWorkspaceAsync(SyncWorkspaceRequest request);
    Task<Result<object>> UpdateWorkspaceAsync(UpdateWorkspaceRequest request);
    Task<Result<object>> QuickPreviewAsync(QuickPreviewRequest request);
    Task<Result<object>> ResolveConflictAsync(ResolveConflictRequest request);
}
