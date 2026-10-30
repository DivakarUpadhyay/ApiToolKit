using ApiToolKit.Application.DTOs.DisplayVideo.CombinedAudience;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.DisplayVideo;

public interface IDisplayVideoCombinedAudienceService
{
    Task<Result<object>> ListCombinedAudiencesAsync(ListCombinedAudiencesRequest request);
    Task<Result<object>> GetCombinedAudienceAsync(GetCombinedAudienceRequest request);
}
