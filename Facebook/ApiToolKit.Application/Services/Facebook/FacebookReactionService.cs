using ApiToolKit.Application.DTOs.Facebook.Reaction;
using ApiToolKit.Application.Interfaces.Services.Facebook;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.Facebook;
using Newtonsoft.Json;

namespace ApiToolKit.Application.Services.Facebook;

public class FacebookReactionService : IFacebookReactionService
{
    private readonly IFacebookAuthRepository _authRepo;
    private readonly IFacebookApiClient _apiClient;

    public FacebookReactionService(IFacebookAuthRepository authRepo, IFacebookApiClient apiClient)
    {
        _authRepo = authRepo;
        _apiClient = apiClient;
    }

    public async Task<Result<bool>> CreateReactionAsync(CreateReactionRequest request, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<bool>.Fail("Facebook configuration not found.");

        var body = new { type = request.Type };
        var response = await _apiClient.PostAsync<dynamic>($"/{request.ObjectId}/reactions", body, accessToken);
        if (response == null) return Result<bool>.Fail("No response from Facebook API.");

        return Result<bool>.Ok(true);
    }

    public async Task<Result<GetReactionsResponse>> GetReactionsAsync(GetReactionsRequest request, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<GetReactionsResponse>.Fail("Facebook configuration not found.");

        var queryParams = new Dictionary<string, string>
        {
            ["fields"] = "id,name,type,pic",
            ["limit"] = request.Limit.ToString(),
            ["summary"] = "true",
            ["access_token"] = accessToken
        };
        if (request.Type != null) queryParams["type"] = request.Type;
        if (request.After != null) queryParams["after"] = request.After;

        var response = await _apiClient.GetAsync<dynamic>($"/{request.ObjectId}/reactions", queryParams);
        if (response == null) return Result<GetReactionsResponse>.Fail("No response from Facebook API.");

        var reactions = JsonConvert.DeserializeObject<List<ReactionResponse>>(
            JsonConvert.SerializeObject(response?.data ?? new List<object>())) ?? new List<ReactionResponse>();
        var summary = new ReactionSummary
        {
            TotalCount = response?.summary?.total_count ?? 0,
            ViewerReaction = response?.summary?.viewer_reaction?.ToString()
        };

        return Result<GetReactionsResponse>.Ok(new GetReactionsResponse { Data = reactions, Summary = summary });
    }

    public async Task<Result<bool>> DeleteReactionAsync(string objectId, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<bool>.Fail("Facebook configuration not found.");

        var response = await _apiClient.DeleteAsync<dynamic>($"/{objectId}/reactions", accessToken);
        if (response == null) return Result<bool>.Fail("No response from Facebook API.");

        return Result<bool>.Ok(true);
    }
}
