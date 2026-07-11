using ApiToolKit.Application.DTOs.Workspace.Chat;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Workspace;

public interface IChatMessageService
{
    Task<Result<ListMessagesResponse>> ListMessagesAsync(string userId, ListMessagesRequest request);
    Task<Result<MessageResponse>> GetMessageAsync(string userId, string messageName);
    Task<Result<MessageResponse>> SendMessageAsync(string userId, SendMessageRequest request);
    Task<Result<MessageResponse>> UpdateMessageAsync(string userId, UpdateMessageRequest request);
    Task<Result<bool>> DeleteMessageAsync(string userId, DeleteMessageRequest request);
}
