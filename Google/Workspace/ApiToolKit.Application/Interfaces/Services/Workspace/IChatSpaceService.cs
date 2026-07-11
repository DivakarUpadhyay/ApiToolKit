using ApiToolKit.Application.DTOs.Workspace.Chat;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Workspace;

public interface IChatSpaceService
{
    Task<Result<ListChatSpacesResponse>> ListSpacesAsync(string userId);
    Task<Result<ChatSpaceResponse>> GetSpaceAsync(string userId, string spaceName);
    Task<Result<ChatSpaceResponse>> CreateSpaceAsync(string userId, CreateChatSpaceRequest request);
    Task<Result<bool>> DeleteSpaceAsync(string userId, string spaceName);
}
