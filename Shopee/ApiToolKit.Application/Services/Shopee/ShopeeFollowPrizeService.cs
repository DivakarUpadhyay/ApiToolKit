using ApiToolKit.Application.DTOs.Shopee.FollowPrize;
using ApiToolKit.Application.Interfaces.Services.Shopee;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.Shopee;

namespace ApiToolKit.Application.Services.Shopee;

public class ShopeeFollowPrizeService : IShopeeFollowPrizeService
{
    private readonly IShopeeAuthRepository _authRepo;
    private readonly IShopeeApiClient _apiClient;

    public ShopeeFollowPrizeService(IShopeeAuthRepository authRepo, IShopeeApiClient apiClient)
    {
        _authRepo = authRepo;
        _apiClient = apiClient;
    }

    private async Task<string?> GetAccessTokenAsync(long shopId)
    {
        var token = await _authRepo.GetTokenByShopIdAsync(shopId);
        return token?.AccessToken;
    }

    public async Task<Result<GetFollowPrizeInfoResponse>> GetFollowPrizeInfoAsync(GetFollowPrizeInfoRequest request)
    {
        var accessToken = await GetAccessTokenAsync(request.ShopId);
        if (accessToken == null) return Result<GetFollowPrizeInfoResponse>.Fail("Access token not found.");

        var queryParams = new Dictionary<string, string>();
        var response = await _apiClient.GetAsync<GetFollowPrizeInfoResponse>("/api/v2/follow_prize/get_follow_prize_info", queryParams, accessToken, request.ShopId);
        if (response == null) return Result<GetFollowPrizeInfoResponse>.Fail("No response from Shopee API.");
        return Result<GetFollowPrizeInfoResponse>.Ok(response);
    }

    public async Task<Result<CreateFollowPrizeResponse>> CreateFollowPrizeAsync(CreateFollowPrizeRequest request)
    {
        var accessToken = await GetAccessTokenAsync(request.ShopId);
        if (accessToken == null) return Result<CreateFollowPrizeResponse>.Fail("Access token not found.");

        var body = new { name = request.Name, start_time = request.StartTime, end_time = request.EndTime, prize_type = request.PrizeType, prize_quantity = request.PrizeQuantity, prize_item_id = request.PrizeItemId };
        var response = await _apiClient.PostAsync<CreateFollowPrizeResponse>("/api/v2/follow_prize/create_follow_prize", body, accessToken, request.ShopId);
        if (response == null) return Result<CreateFollowPrizeResponse>.Fail("No response from Shopee API.");
        return Result<CreateFollowPrizeResponse>.Ok(response);
    }

    public async Task<Result<UpdateFollowPrizeResponse>> UpdateFollowPrizeAsync(UpdateFollowPrizeRequest request)
    {
        var accessToken = await GetAccessTokenAsync(request.ShopId);
        if (accessToken == null) return Result<UpdateFollowPrizeResponse>.Fail("Access token not found.");

        var body = new { follow_prize_id = request.FollowPrizeId, name = request.Name, start_time = request.StartTime, end_time = request.EndTime };
        var response = await _apiClient.PostAsync<UpdateFollowPrizeResponse>("/api/v2/follow_prize/update_follow_prize", body, accessToken, request.ShopId);
        if (response == null) return Result<UpdateFollowPrizeResponse>.Fail("No response from Shopee API.");
        return Result<UpdateFollowPrizeResponse>.Ok(response);
    }

    public async Task<Result<EndFollowPrizeResponse>> EndFollowPrizeAsync(EndFollowPrizeRequest request)
    {
        var accessToken = await GetAccessTokenAsync(request.ShopId);
        if (accessToken == null) return Result<EndFollowPrizeResponse>.Fail("Access token not found.");

        var body = new { follow_prize_id = request.FollowPrizeId };
        var response = await _apiClient.PostAsync<EndFollowPrizeResponse>("/api/v2/follow_prize/end_follow_prize", body, accessToken, request.ShopId);
        if (response == null) return Result<EndFollowPrizeResponse>.Fail("No response from Shopee API.");
        return Result<EndFollowPrizeResponse>.Ok(response);
    }

    public async Task<Result<GetFollowPrizeParticipantListResponse>> GetFollowPrizeParticipantListAsync(GetFollowPrizeParticipantListRequest request)
    {
        var accessToken = await GetAccessTokenAsync(request.ShopId);
        if (accessToken == null) return Result<GetFollowPrizeParticipantListResponse>.Fail("Access token not found.");

        var queryParams = new Dictionary<string, string> { ["follow_prize_id"] = request.FollowPrizeId.ToString(), ["page_no"] = request.PageNo.ToString(), ["page_size"] = request.PageSize.ToString() };
        var response = await _apiClient.GetAsync<GetFollowPrizeParticipantListResponse>("/api/v2/follow_prize/get_follow_prize_participant_list", queryParams, accessToken, request.ShopId);
        if (response == null) return Result<GetFollowPrizeParticipantListResponse>.Fail("No response from Shopee API.");
        return Result<GetFollowPrizeParticipantListResponse>.Ok(response);
    }

    public async Task<Result<GetFollowPrizeWinnerListResponse>> GetFollowPrizeWinnerListAsync(GetFollowPrizeWinnerListRequest request)
    {
        var accessToken = await GetAccessTokenAsync(request.ShopId);
        if (accessToken == null) return Result<GetFollowPrizeWinnerListResponse>.Fail("Access token not found.");

        var queryParams = new Dictionary<string, string> { ["follow_prize_id"] = request.FollowPrizeId.ToString(), ["page_no"] = request.PageNo.ToString(), ["page_size"] = request.PageSize.ToString() };
        var response = await _apiClient.GetAsync<GetFollowPrizeWinnerListResponse>("/api/v2/follow_prize/get_follow_prize_winner_list", queryParams, accessToken, request.ShopId);
        if (response == null) return Result<GetFollowPrizeWinnerListResponse>.Fail("No response from Shopee API.");
        return Result<GetFollowPrizeWinnerListResponse>.Ok(response);
    }
}
