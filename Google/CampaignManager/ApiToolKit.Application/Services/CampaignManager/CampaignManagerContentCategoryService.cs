using ApiToolKit.Application.DTOs.CampaignManager.ContentCategory;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.CampaignManager;

namespace ApiToolKit.Application.Services.CampaignManager;

public class CampaignManagerContentCategoryService : CampaignManagerServiceBase, ICampaignManagerContentCategoryService
{
    public CampaignManagerContentCategoryService(ICampaignManagerApiClient apiClient, ICampaignManagerAuthService authService, ICampaignManagerAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> DeleteContentCategoryAsync(DeleteContentCategoryRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/contentCategories/{Esc(r.ContentCategoryId)}";
        return await SendAsync(r.AccountKey, "DeleteContentCategory", HttpMethod.Delete, url);
    }

    public async Task<Result<object>> ListContentCategoriesAsync(ListContentCategoriesRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/contentCategories";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "maxResults", r.MaxResults?.ToString());
        Add(query, "sortField", r.SortField);
        Add(query, "pageToken", r.PageToken);
        Add(query, "searchString", r.SearchString);
        AddMany(query, "ids", r.Ids);
        Add(query, "sortOrder", r.SortOrder);
        return await SendAsync(r.AccountKey, "ListContentCategories", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> UpdateContentCategoryAsync(UpdateContentCategoryRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/contentCategories";
        return await SendAsync(r.AccountKey, "UpdateContentCategory", HttpMethod.Put, url, null, r.Body);
    }

    public async Task<Result<object>> GetContentCategoryAsync(GetContentCategoryRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/contentCategories/{Esc(r.ContentCategoryId)}";
        return await SendAsync(r.AccountKey, "GetContentCategory", HttpMethod.Get, url);
    }

    public async Task<Result<object>> InsertContentCategoryAsync(InsertContentCategoryRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/contentCategories";
        return await SendAsync(r.AccountKey, "InsertContentCategory", HttpMethod.Post, url, null, r.Body);
    }

    public async Task<Result<object>> PatchContentCategoryAsync(PatchContentCategoryRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/contentCategories";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "id", r.Id);
        return await SendAsync(r.AccountKey, "PatchContentCategory", HttpMethod.Patch, url, query, r.Body);
    }
}
