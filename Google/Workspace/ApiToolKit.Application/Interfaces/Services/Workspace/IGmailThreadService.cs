using ApiToolKit.Application.DTOs.Workspace.Gmail;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Workspace;

public interface IGmailThreadService
{
    Task<Result<ListThreadsResponse>> ListThreadsAsync(string userId, ListThreadsRequest request);
    Task<Result<ThreadResponse>> GetThreadAsync(string userId, string threadId);
    Task<Result<ThreadResponse>> ModifyThreadAsync(string userId, ModifyThreadRequest request);
    Task<Result<GmailMessageResponse>> TrashThreadAsync(string userId, string threadId);
    Task<Result<GmailMessageResponse>> UntrashThreadAsync(string userId, string threadId);
    Task<Result<bool>> DeleteThreadAsync(string userId, DeleteThreadRequest request);
}
