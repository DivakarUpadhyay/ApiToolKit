using ApiToolKit.Application.DTOs.DisplayVideo.FloodlightGroupFloodlightActivity;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.DisplayVideo;

public interface IDisplayVideoFloodlightGroupFloodlightActivityService
{
    Task<Result<object>> ListFloodlightActivitiesAsync(ListFloodlightActivitiesRequest request);
    Task<Result<object>> GetFloodlightActivityAsync(GetFloodlightActivityRequest request);
}
