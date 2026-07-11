using ApiToolKit.Domain.Entities.Facebook;

namespace ApiToolKit.Domain.Interfaces.Repositories.Facebook;

public interface IFacebookMessengerRepository
{
    Task<int> SaveMessageAsync(FacebookMessenger message);
    Task<IEnumerable<FacebookMessenger>> GetMessagesByConversationAsync(string senderId, string recipientId, int limit, int offset);
    Task<int> SaveApiLogAsync(FacebookApiLog log);
}
