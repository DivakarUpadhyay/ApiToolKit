using ApiToolKit.Domain.Entities.Firebase;

namespace ApiToolKit.Domain.Interfaces.Repositories.Firebase;

public interface IFcmRepository
{
    Task<int> SaveMessageAsync(FcmMessage message);
    Task<int> UpdateMessageStatusAsync(string messageId, string status);
    Task<List<FcmMessage>> GetMessagesByProjectIdAsync(string projectId);
}
