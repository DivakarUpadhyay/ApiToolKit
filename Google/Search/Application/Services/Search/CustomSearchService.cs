using ApiToolKit.Application.DTOs.Search.CustomSearch;
using ApiToolKit.Application.Interfaces.Services.Search;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Entities.Search;
using ApiToolKit.Domain.Interfaces.Repositories.Search;
using Newtonsoft.Json;

namespace ApiToolKit.Application.Services.Search;

public class CustomSearchService : ICustomSearchService
{
    private readonly ISearchApiClient _apiClient;
    private readonly ISearchConfigRepository _configRepo;
    private readonly ICustomSearchRepository _searchRepo;
    private const string BaseUrl = "https://www.googleapis.com/customsearch/v1";

    public CustomSearchService(
        ISearchApiClient apiClient,
        ISearchConfigRepository configRepo,
        ICustomSearchRepository searchRepo)
    {
        _apiClient = apiClient;
        _configRepo = configRepo;
        _searchRepo = searchRepo;
    }

    public async Task<Result<CustomSearchResponse>> SearchAsync(CustomSearchRequest request)
    {
        var config = await _configRepo.GetConfigAsync();
        if (config == null)
            return Result<CustomSearchResponse>.Fail("Search configuration not found.");

        var cx = request.Cx ?? config.SearchEngineId;

        var queryParams = new Dictionary<string, string>
        {
            ["q"] = request.Q,
            ["cx"] = cx,
            ["num"] = request.Num.ToString(),
            ["start"] = request.Start.ToString(),
            ["hl"] = request.Language,
            ["filter"] = request.Filter,
            ["safe"] = request.Safe
        };

        if (!string.IsNullOrEmpty(request.CountryRestrict)) queryParams["cr"] = request.CountryRestrict;
        if (!string.IsNullOrEmpty(request.DateRestrict)) queryParams["dateRestrict"] = request.DateRestrict;
        if (!string.IsNullOrEmpty(request.ExactTerms)) queryParams["exactTerms"] = request.ExactTerms;
        if (!string.IsNullOrEmpty(request.ExcludeTerms)) queryParams["excludeTerms"] = request.ExcludeTerms;
        if (!string.IsNullOrEmpty(request.FileType)) queryParams["fileType"] = request.FileType;
        if (!string.IsNullOrEmpty(request.Gl)) queryParams["gl"] = request.Gl;
        if (!string.IsNullOrEmpty(request.Hq)) queryParams["hq"] = request.Hq;
        if (!string.IsNullOrEmpty(request.LinkSite)) queryParams["linkSite"] = request.LinkSite;
        if (!string.IsNullOrEmpty(request.LowRange)) queryParams["lowRange"] = request.LowRange;
        if (!string.IsNullOrEmpty(request.HighRange)) queryParams["highRange"] = request.HighRange;
        if (!string.IsNullOrEmpty(request.OrTerms)) queryParams["orTerms"] = request.OrTerms;
        if (!string.IsNullOrEmpty(request.Rights)) queryParams["rights"] = request.Rights;
        if (!string.IsNullOrEmpty(request.SearchType)) queryParams["searchType"] = request.SearchType;
        if (!string.IsNullOrEmpty(request.SiteSearch)) queryParams["siteSearch"] = request.SiteSearch;
        if (!string.IsNullOrEmpty(request.SiteSearchFilter)) queryParams["siteSearchFilter"] = request.SiteSearchFilter;
        if (!string.IsNullOrEmpty(request.Sort)) queryParams["sort"] = request.Sort;

        var response = await _apiClient.GetAsync<CustomSearchResponse>(BaseUrl, queryParams);
        if (response == null)
            return Result<CustomSearchResponse>.Fail("No response from Custom Search API.");

        long totalResults = 0;
        if (long.TryParse(response.SearchInformation?.TotalResults, out var tr))
            totalResults = tr;

        var dbResult = new CustomSearchResult
        {
            Query = request.Q,
            SearchEngineId = cx,
            TotalResults = totalResults,
            SearchType = "web",
            ResultsJson = JsonConvert.SerializeObject(response),
            CreatedAt = DateTime.UtcNow
        };
        await _searchRepo.SaveSearchResultAsync(dbResult);

        return Result<CustomSearchResponse>.Ok(response);
    }

    public async Task<Result<CustomSearchResponse>> ImageSearchAsync(ImageSearchRequest request)
    {
        var config = await _configRepo.GetConfigAsync();
        if (config == null)
            return Result<CustomSearchResponse>.Fail("Search configuration not found.");

        var cx = request.Cx ?? config.SearchEngineId;

        var queryParams = new Dictionary<string, string>
        {
            ["q"] = request.Q,
            ["cx"] = cx,
            ["searchType"] = "image",
            ["num"] = request.Num.ToString(),
            ["start"] = request.Start.ToString(),
            ["safe"] = request.Safe
        };

        if (!string.IsNullOrEmpty(request.Gl)) queryParams["gl"] = request.Gl;
        if (!string.IsNullOrEmpty(request.Language)) queryParams["hl"] = request.Language;
        if (!string.IsNullOrEmpty(request.ImageColorType)) queryParams["imgColorType"] = request.ImageColorType;
        if (!string.IsNullOrEmpty(request.ImageDominantColor)) queryParams["imgDominantColor"] = request.ImageDominantColor;
        if (!string.IsNullOrEmpty(request.ImageSize)) queryParams["imgSize"] = request.ImageSize;
        if (!string.IsNullOrEmpty(request.ImageType)) queryParams["imgType"] = request.ImageType;
        if (!string.IsNullOrEmpty(request.FileType)) queryParams["fileType"] = request.FileType;
        if (!string.IsNullOrEmpty(request.Rights)) queryParams["rights"] = request.Rights;

        var response = await _apiClient.GetAsync<CustomSearchResponse>(BaseUrl, queryParams);
        if (response == null)
            return Result<CustomSearchResponse>.Fail("No response from Custom Search API.");

        long totalResults = 0;
        if (long.TryParse(response.SearchInformation?.TotalResults, out var tr))
            totalResults = tr;

        var dbResult = new CustomSearchResult
        {
            Query = request.Q,
            SearchEngineId = cx,
            TotalResults = totalResults,
            SearchType = "image",
            ResultsJson = JsonConvert.SerializeObject(response),
            CreatedAt = DateTime.UtcNow
        };
        await _searchRepo.SaveSearchResultAsync(dbResult);

        return Result<CustomSearchResponse>.Ok(response);
    }
}
