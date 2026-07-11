using ApiToolKit.Application.DTOs.Search.KnowledgeGraph;
using ApiToolKit.Application.Interfaces.Services.Search;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Entities.Search;
using ApiToolKit.Domain.Interfaces.Repositories.Search;
using Newtonsoft.Json;

namespace ApiToolKit.Application.Services.Search;

public class KnowledgeGraphService : IKnowledgeGraphService
{
    private readonly ISearchApiClient _apiClient;
    private readonly ISearchConfigRepository _configRepo;
    private readonly IKnowledgeGraphRepository _kgRepo;
    private const string BaseUrl = "https://kgsearch.googleapis.com/v1/entities:search";

    public KnowledgeGraphService(
        ISearchApiClient apiClient,
        ISearchConfigRepository configRepo,
        IKnowledgeGraphRepository kgRepo)
    {
        _apiClient = apiClient;
        _configRepo = configRepo;
        _kgRepo = kgRepo;
    }

    public async Task<Result<KnowledgeGraphResponse>> SearchAsync(KnowledgeGraphRequest request)
    {
        var config = await _configRepo.GetConfigAsync();
        if (config == null)
            return Result<KnowledgeGraphResponse>.Fail("Search configuration not found.");

        var queryParams = new Dictionary<string, string>
        {
            ["query"] = request.Query,
            ["languageCode"] = request.LanguageCode,
            ["limit"] = request.Limit.ToString(),
            ["indent"] = request.Indent.ToString().ToLower(),
            ["prefix"] = request.Prefix.ToString().ToLower()
        };

        if (request.Types != null && request.Types.Count > 0)
            queryParams["types"] = string.Join(",", request.Types);

        var response = await _apiClient.GetAsync<KnowledgeGraphResponse>(BaseUrl, queryParams);
        if (response == null)
            return Result<KnowledgeGraphResponse>.Fail("No response from Knowledge Graph API.");

        var dbResult = new KnowledgeGraphResult
        {
            Query = request.Query,
            Types = request.Types != null ? string.Join(",", request.Types) : null,
            ResultsJson = JsonConvert.SerializeObject(response),
            CreatedAt = DateTime.UtcNow
        };
        await _kgRepo.SaveSearchResultAsync(dbResult);

        return Result<KnowledgeGraphResponse>.Ok(response);
    }
}
