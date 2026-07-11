using ApiToolKit.Application.DTOs.Workspace.Gmail;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Workspace;

public interface IGmailDraftService
{
    Task<Result<ListDraftsResponse>> ListDraftsAsync(string userId);
    Task<Result<DraftResponse>> GetDraftAsync(string userId, string draftId);
    Task<Result<DraftResponse>> CreateDraftAsync(string userId, CreateDraftRequest request);
    Task<Result<DraftResponse>> UpdateDraftAsync(string userId, UpdateDraftRequest request);
    Task<Result<GmailMessageResponse>> SendDraftAsync(string userId, SendDraftRequest request);
    Task<Result<bool>> DeleteDraftAsync(string userId, DeleteDraftRequest request);
}
