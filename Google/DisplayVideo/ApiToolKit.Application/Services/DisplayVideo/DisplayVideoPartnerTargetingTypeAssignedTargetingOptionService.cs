using ApiToolKit.Application.DTOs.DisplayVideo.PartnerTargetingTypeAssignedTargetingOption;
using ApiToolKit.Application.Interfaces.Services.DisplayVideo;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.DisplayVideo;

namespace ApiToolKit.Application.Services.DisplayVideo;

public class DisplayVideoPartnerTargetingTypeAssignedTargetingOptionService : DisplayVideoServiceBase, IDisplayVideoPartnerTargetingTypeAssignedTargetingOptionService
{
    public DisplayVideoPartnerTargetingTypeAssignedTargetingOptionService(IDisplayVideoApiClient apiClient, IDisplayVideoAuthService authService, IDisplayVideoAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> CreateAssignedTargetingOptionAsync(CreateAssignedTargetingOptionRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/partners/{Esc(r.PartnerId)}/targetingTypes/{Esc(r.TargetingTypeId)}/assignedTargetingOptions";
        return await SendAsync(r.AccountKey, "CreateAssignedTargetingOption", HttpMethod.Post, url, null, r.Body);
    }

    public async Task<Result<object>> DeleteAssignedTargetingOptionAsync(DeleteAssignedTargetingOptionRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/partners/{Esc(r.PartnerId)}/targetingTypes/{Esc(r.TargetingTypeId)}/assignedTargetingOptions/{Esc(r.AssignedTargetingOptionId)}";
        return await SendAsync(r.AccountKey, "DeleteAssignedTargetingOption", HttpMethod.Delete, url);
    }

    public async Task<Result<object>> GetAssignedTargetingOptionAsync(GetAssignedTargetingOptionRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/partners/{Esc(r.PartnerId)}/targetingTypes/{Esc(r.TargetingTypeId)}/assignedTargetingOptions/{Esc(r.AssignedTargetingOptionId)}";
        return await SendAsync(r.AccountKey, "GetAssignedTargetingOption", HttpMethod.Get, url);
    }

    public async Task<Result<object>> ListAssignedTargetingOptionsAsync(ListAssignedTargetingOptionsRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/partners/{Esc(r.PartnerId)}/targetingTypes/{Esc(r.TargetingTypeId)}/assignedTargetingOptions";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "orderBy", r.OrderBy);
        Add(query, "filter", r.Filter);
        Add(query, "pageSize", r.PageSize?.ToString());
        Add(query, "pageToken", r.PageToken);
        return await SendAsync(r.AccountKey, "ListAssignedTargetingOptions", HttpMethod.Get, url, query);
    }
}
