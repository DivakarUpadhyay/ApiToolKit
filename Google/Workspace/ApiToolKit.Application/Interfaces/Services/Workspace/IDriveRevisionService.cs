using ApiToolKit.Application.DTOs.Workspace.Drive;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Workspace;

public interface IDriveRevisionService
{
    Task<Result<ListRevisionsResponse>> ListRevisionsAsync(string userId, string fileId);
    Task<Result<RevisionResponse>> GetRevisionAsync(string userId, string fileId, string revisionId);
    Task<Result<RevisionResponse>> UpdateRevisionAsync(string userId, UpdateRevisionRequest request);
    Task<Result<bool>> DeleteRevisionAsync(string userId, DeleteRevisionRequest request);
}
