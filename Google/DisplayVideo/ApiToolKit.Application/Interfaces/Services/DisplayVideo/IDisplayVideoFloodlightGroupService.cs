using ApiToolKit.Application.DTOs.DisplayVideo.FloodlightGroup;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.DisplayVideo;

public interface IDisplayVideoFloodlightGroupService
{
    Task<Result<object>> UpdateFloodlightGroupAsync(UpdateFloodlightGroupRequest request);
    Task<Result<object>> GetFloodlightGroupAsync(GetFloodlightGroupRequest request);
}
