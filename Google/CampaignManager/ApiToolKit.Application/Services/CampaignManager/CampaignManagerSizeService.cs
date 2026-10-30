using ApiToolKit.Application.DTOs.CampaignManager.Size;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.CampaignManager;

namespace ApiToolKit.Application.Services.CampaignManager;

public class CampaignManagerSizeService : CampaignManagerServiceBase, ICampaignManagerSizeService
{
    public CampaignManagerSizeService(ICampaignManagerApiClient apiClient, ICampaignManagerAuthService authService, ICampaignManagerAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> ListSizesAsync(ListSizesRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/sizes";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "height", r.Height?.ToString());
        AddMany(query, "ids", r.Ids);
        Add(query, "width", r.Width?.ToString());
        Add(query, "iabStandard", r.IabStandard?.ToString().ToLower());
        return await SendAsync(r.AccountKey, "ListSizes", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> InsertSizeAsync(InsertSizeRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/sizes";
        return await SendAsync(r.AccountKey, "InsertSize", HttpMethod.Post, url, null, r.Body);
    }

    public async Task<Result<object>> GetSizeAsync(GetSizeRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/sizes/{Esc(r.SizeId)}";
        return await SendAsync(r.AccountKey, "GetSize", HttpMethod.Get, url);
    }
}
