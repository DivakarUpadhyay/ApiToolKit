using ApiToolKit.Application.DTOs.Facebook.Event;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Facebook;

public interface IFacebookEventService
{
    Task<Result<CreateEventResponse>> CreateEventAsync(CreateEventRequest request, string accessToken);
    Task<Result<EventResponse>> GetEventAsync(string eventId, string accessToken);
    Task<Result<bool>> UpdateEventAsync(UpdateEventRequest request, string accessToken);
    Task<Result<bool>> DeleteEventAsync(string eventId, string accessToken);
    Task<Result<List<EventResponse>>> GetEventsByPageAsync(string pageId, string accessToken, int limit);
    Task<Result<List<EventAttendeeResponse>>> GetEventAttendeesAsync(GetEventAttendeesRequest request, string accessToken);
    Task<Result<bool>> RsvpEventAsync(RsvpEventRequest request, string accessToken);
}
