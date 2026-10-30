using ApiToolKit.Application.DTOs.DisplayVideo.Sdfdownloadtask;
using ApiToolKit.Application.Interfaces.Services.DisplayVideo;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.DisplayVideo;

namespace ApiToolKit.Application.Services.DisplayVideo;

public class DisplayVideoSdfdownloadtaskService : DisplayVideoServiceBase, IDisplayVideoSdfdownloadtaskService
{
    public DisplayVideoSdfdownloadtaskService(IDisplayVideoApiClient apiClient, IDisplayVideoAuthService authService, IDisplayVideoAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> CreateSdfdownloadtaskAsync(CreateSdfdownloadtaskRequest r)
    {
        var url = "https://displayvideo.googleapis.com/v4/sdfdownloadtasks";
        return await SendAsync(r.AccountKey, "CreateSdfdownloadtask", HttpMethod.Post, url, null, r.Body);
    }
}
