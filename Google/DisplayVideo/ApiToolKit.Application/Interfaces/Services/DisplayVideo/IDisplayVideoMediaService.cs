using ApiToolKit.Application.DTOs.DisplayVideo.Media;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.DisplayVideo;

public interface IDisplayVideoMediaService
{
    Task<Result<object>> DownloadMediaAsync(DownloadMediaRequest request);
    Task<Result<object>> DownloadMediaContentAsync(DownloadMediaContentRequest request);
}
