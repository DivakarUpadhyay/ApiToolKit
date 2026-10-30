using ApiToolKit.Application.DTOs.CampaignManager.VideoFormat;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.CampaignManager;

public interface ICampaignManagerVideoFormatService
{
    Task<Result<object>> ListVideoFormatsAsync(ListVideoFormatsRequest request);
    Task<Result<object>> GetVideoFormatAsync(GetVideoFormatRequest request);
}
