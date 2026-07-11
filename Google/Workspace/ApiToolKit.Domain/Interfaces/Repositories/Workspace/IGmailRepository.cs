using ApiToolKit.Domain.Entities.Workspace.Gmail;

namespace ApiToolKit.Domain.Interfaces.Repositories.Workspace;

public interface IGmailRepository
{
    Task SaveMessageAsync(GmailMessage message);
    Task<GmailMessage?> GetMessageByIdAsync(string messageId, string userId);
    Task SaveLabelAsync(GmailLabel label);
    Task<List<GmailLabel>> GetLabelsByUserIdAsync(string userId);
}
