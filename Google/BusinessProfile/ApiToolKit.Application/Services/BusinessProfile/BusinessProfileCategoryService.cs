using ApiToolKit.Application.DTOs.BusinessProfile.Category;
using ApiToolKit.Application.Interfaces.Services.BusinessProfile;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.BusinessProfile;

namespace ApiToolKit.Application.Services.BusinessProfile;

public class BusinessProfileCategoryService : BusinessProfileServiceBase, IBusinessProfileCategoryService
{
    public BusinessProfileCategoryService(IBusinessProfileApiClient apiClient, IBusinessProfileAuthService authService, IBusinessProfileAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> ListCategoriesAsync(ListCategoriesRequest r)
    {
        var url = "https://mybusinessbusinessinformation.googleapis.com/v1/categories";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "regionCode", r.RegionCode);
        Add(query, "languageCode", r.LanguageCode);
        Add(query, "filter", r.Filter);
        Add(query, "view", "BASIC");
        Add(query, "pageSize", r.PageSize?.ToString());
        Add(query, "pageToken", r.PageToken);
        return await SendAsync(r.AccountKey, "ListCategories", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> BatchGetCategoriesAsync(BatchGetCategoriesRequest r)
    {
        var url = "https://mybusinessbusinessinformation.googleapis.com/v1/categories:batchGet";
        var query = new List<KeyValuePair<string, string?>>();
        AddMany(query, "names", r.Names);
        Add(query, "languageCode", r.LanguageCode);
        Add(query, "regionCode", r.RegionCode);
        Add(query, "view", "FULL");
        return await SendAsync(r.AccountKey, "BatchGetCategories", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> ListAttributeMetadataAsync(ListAttributeMetadataRequest r)
    {
        var url = "https://mybusinessbusinessinformation.googleapis.com/v1/attributes";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "categoryName", r.CategoryName);
        Add(query, "parent", r.Parent);
        Add(query, "regionCode", r.RegionCode);
        Add(query, "languageCode", r.LanguageCode);
        Add(query, "pageSize", r.PageSize?.ToString());
        Add(query, "pageToken", r.PageToken);
        return await SendAsync(r.AccountKey, "ListAttributeMetadata", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> GetChainAsync(GetChainRequest r)
    {
        var url = $"https://mybusinessbusinessinformation.googleapis.com/v1/chains/{r.ChainId}";
        return await SendAsync(r.AccountKey, "GetChain", HttpMethod.Get, url);
    }

    public async Task<Result<object>> SearchChainsAsync(SearchChainsRequest r)
    {
        var url = "https://mybusinessbusinessinformation.googleapis.com/v1/chains:search";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "chainName", r.ChainName);
        Add(query, "pageSize", r.PageSize?.ToString());
        return await SendAsync(r.AccountKey, "SearchChains", HttpMethod.Get, url, query);
    }
}
