using ApiToolKit.Domain.Entities.Workspace.Chat;

namespace ApiToolKit.Domain.Interfaces.Repositories.Workspace;

public interface IChatRepository
{
    Task SaveMessageAsync(ChatMessage message);
    Task<List<ChatMessage>> GetMessagesBySpaceAsync(string spaceName);
}
