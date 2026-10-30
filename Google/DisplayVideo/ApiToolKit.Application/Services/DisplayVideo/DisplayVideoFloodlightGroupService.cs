using ApiToolKit.Application.DTOs.DisplayVideo.FloodlightGroup;
using ApiToolKit.Application.Interfaces.Services.DisplayVideo;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.DisplayVideo;

namespace ApiToolKit.Application.Services.DisplayVideo;

public class DisplayVideoFloodlightGroupService : DisplayVideoServiceBase, IDisplayVideoFloodlightGroupService
{
    public DisplayVideoFloodlightGroupService(IDisplayVideoApiClient apiClient, IDisplayVideoAuthService authService, IDisplayVideoAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> UpdateFloodlightGroupAsync(UpdateFloodlightGroupRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/floodlightGroups/{Esc(r.FloodlightGroupId)}";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "partnerId", r.PartnerId);
        Add(query, "updateMask", r.UpdateMask);
        return await SendAsync(r.AccountKey, "UpdateFloodlightGroup", HttpMethod.Patch, url, query, r.Body);
    }

    public async Task<Result<object>> GetFloodlightGroupAsync(GetFloodlightGroupRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/floodlightGroups/{Esc(r.FloodlightGroupId)}";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "partnerId", r.PartnerId);
        return await SendAsync(r.AccountKey, "GetFloodlightGroup", HttpMethod.Get, url, query);
    }
}
