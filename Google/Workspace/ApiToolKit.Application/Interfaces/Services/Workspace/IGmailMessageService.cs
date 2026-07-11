using ApiToolKit.Application.DTOs.Workspace.Gmail;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Workspace;

public interface IGmailMessageService
{
    Task<Result<ListMessagesResponse>> ListMessagesAsync(string userId, ListMessagesRequest request);
    Task<Result<GmailMessageResponse>> GetMessageAsync(string userId, string messageId);
    Task<Result<GmailMessageResponse>> SendMessageAsync(string userId, SendMessageRequest request);
    Task<Result<GmailMessageResponse>> ModifyMessageAsync(string userId, ModifyMessageRequest request);
    Task<Result<bool>> BatchModifyAsync(string userId, BatchModifyRequest request);
    Task<Result<bool>> DeleteMessageAsync(string userId, DeleteMessageRequest request);
    Task<Result<GmailMessageResponse>> TrashMessageAsync(string userId, TrashMessageRequest request);
    Task<Result<GmailMessageResponse>> UntrashMessageAsync(string userId, string messageId);
    Task<Result<object>> GetAttachmentAsync(string userId, GetAttachmentRequest request);
}
