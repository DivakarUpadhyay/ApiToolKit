using ApiToolKit.Domain.Entities.Facebook;

namespace ApiToolKit.Domain.Interfaces.Repositories.Facebook;

public interface IFacebookWebhookRepository
{
    Task<int> SaveWebhookAsync(FacebookWebhook webhook);
    Task<FacebookWebhook?> GetWebhookByIdAsync(int id);
    Task<IEnumerable<FacebookWebhook>> GetWebhooksByObjectTypeAsync(string objectType);
    Task<int> DeleteWebhookAsync(int id);
    Task<int> UpdateWebhookAsync(FacebookWebhook webhook);
    Task<int> SaveApiLogAsync(FacebookApiLog log);
}
