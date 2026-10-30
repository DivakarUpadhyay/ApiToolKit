using ApiToolKit.Application.DTOs.DisplayVideo.GoogleAudience;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.DisplayVideo;

public interface IDisplayVideoGoogleAudienceService
{
    Task<Result<object>> ListGoogleAudiencesAsync(ListGoogleAudiencesRequest request);
    Task<Result<object>> GetGoogleAudienceAsync(GetGoogleAudienceRequest request);
}
