using ApiToolKit.Domain.Entities.Facebook;

namespace ApiToolKit.Domain.Interfaces.Repositories.Facebook;

public interface IFacebookEventRepository
{
    Task<int> SaveEventAsync(FacebookEvent facebookEvent);
    Task<FacebookEvent?> GetEventByIdAsync(string eventId);
    Task<IEnumerable<FacebookEvent>> GetEventsByPageIdAsync(string pageId, int limit, int offset);
    Task<int> UpdateEventAsync(FacebookEvent facebookEvent);
    Task<int> DeleteEventAsync(string eventId);
    Task<int> SaveApiLogAsync(FacebookApiLog log);
}
