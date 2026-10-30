using ApiToolKit.Application.DTOs.CampaignManager.VideoFormat;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.CampaignManager;

namespace ApiToolKit.Application.Services.CampaignManager;

public class CampaignManagerVideoFormatService : CampaignManagerServiceBase, ICampaignManagerVideoFormatService
{
    public CampaignManagerVideoFormatService(ICampaignManagerApiClient apiClient, ICampaignManagerAuthService authService, ICampaignManagerAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> ListVideoFormatsAsync(ListVideoFormatsRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/videoFormats";
        return await SendAsync(r.AccountKey, "ListVideoFormats", HttpMethod.Get, url);
    }

    public async Task<Result<object>> GetVideoFormatAsync(GetVideoFormatRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/videoFormats/{Esc(r.VideoFormatId)}";
        return await SendAsync(r.AccountKey, "GetVideoFormat", HttpMethod.Get, url);
    }
}
