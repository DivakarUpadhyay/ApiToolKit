using ApiToolKit.Application.DTOs.DisplayVideo.Media;
using ApiToolKit.Application.Interfaces.Services.DisplayVideo;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.DisplayVideo;

namespace ApiToolKit.Application.Services.DisplayVideo;

public class DisplayVideoMediaService : DisplayVideoServiceBase, IDisplayVideoMediaService
{
    public DisplayVideoMediaService(IDisplayVideoApiClient apiClient, IDisplayVideoAuthService authService, IDisplayVideoAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> DownloadMediaAsync(DownloadMediaRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/download/{Esc(r.DownloadId)}";
        return await SendAsync(r.AccountKey, "DownloadMedia", HttpMethod.Get, url);
    }

    public async Task<Result<object>> DownloadMediaContentAsync(DownloadMediaContentRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/download/{Esc(r.DownloadId)}";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "alt", "media");
        return await SendAsync(r.AccountKey, "DownloadMediaContent", HttpMethod.Get, url, query, null, raw: true);
    }
}
