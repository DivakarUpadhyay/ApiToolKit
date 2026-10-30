using ApiToolKit.Application.DTOs.DisplayVideo.SdfdownloadtaskOperation;
using ApiToolKit.Application.Interfaces.Services.DisplayVideo;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.DisplayVideo;

namespace ApiToolKit.Application.Services.DisplayVideo;

public class DisplayVideoSdfdownloadtaskOperationService : DisplayVideoServiceBase, IDisplayVideoSdfdownloadtaskOperationService
{
    public DisplayVideoSdfdownloadtaskOperationService(IDisplayVideoApiClient apiClient, IDisplayVideoAuthService authService, IDisplayVideoAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> GetOperationAsync(GetOperationRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/sdfdownloadtasks/operations/{Esc(r.OperationId)}";
        return await SendAsync(r.AccountKey, "GetOperation", HttpMethod.Get, url);
    }
}
