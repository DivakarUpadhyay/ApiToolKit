using ApiToolKit.Application.DTOs.Shopee.Livestream;
using ApiToolKit.Application.Interfaces.Services.Shopee;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.Shopee;

namespace ApiToolKit.Application.Services.Shopee;

public class ShopeeLivestreamService : IShopeeLivestreamService
{
    private readonly IShopeeAuthRepository _authRepo;
    private readonly IShopeeApiClient _apiClient;

    public ShopeeLivestreamService(IShopeeAuthRepository authRepo, IShopeeApiClient apiClient)
    {
        _authRepo = authRepo;
        _apiClient = apiClient;
    }

    private async Task<string?> GetAccessTokenAsync(long shopId)
    {
        var token = await _authRepo.GetTokenByShopIdAsync(shopId);
        return token?.AccessToken;
    }

    public async Task<Result<CreateLivestreamResponse>> CreateLivestreamAsync(CreateLivestreamRequest request)
    {
        var accessToken = await GetAccessTokenAsync(request.ShopId);
        if (accessToken == null) return Result<CreateLivestreamResponse>.Fail("Access token not found.");

        var body = new { title = request.Title, description = request.Description, scheduled_start_time = request.ScheduledStartTime, cover_image = request.CoverImage };
        var response = await _apiClient.PostAsync<CreateLivestreamResponse>("/api/v2/live/create_live", body, accessToken, request.ShopId);
        if (response == null) return Result<CreateLivestreamResponse>.Fail("No response from Shopee API.");
        return Result<CreateLivestreamResponse>.Ok(response);
    }

    public async Task<Result<GetLivestreamDetailResponse>> GetLivestreamDetailAsync(GetLivestreamDetailRequest request)
    {
        var accessToken = await GetAccessTokenAsync(request.ShopId);
        if (accessToken == null) return Result<GetLivestreamDetailResponse>.Fail("Access token not found.");

        var queryParams = new Dictionary<string, string> { ["livestream_id"] = request.LivestreamId.ToString() };
        var response = await _apiClient.GetAsync<GetLivestreamDetailResponse>("/api/v2/live/get_live_detail", queryParams, accessToken, request.ShopId);
        if (response == null) return Result<GetLivestreamDetailResponse>.Fail("No response from Shopee API.");
        return Result<GetLivestreamDetailResponse>.Ok(response);
    }

    public async Task<Result<UpdateLivestreamResponse>> UpdateLivestreamAsync(UpdateLivestreamRequest request)
    {
        var accessToken = await GetAccessTokenAsync(request.ShopId);
        if (accessToken == null) return Result<UpdateLivestreamResponse>.Fail("Access token not found.");

        var body = new { livestream_id = request.LivestreamId, title = request.Title, description = request.Description, cover_image = request.CoverImage };
        var response = await _apiClient.PostAsync<UpdateLivestreamResponse>("/api/v2/live/update_live", body, accessToken, request.ShopId);
        if (response == null) return Result<UpdateLivestreamResponse>.Fail("No response from Shopee API.");
        return Result<UpdateLivestreamResponse>.Ok(response);
    }

    public async Task<Result<EndLivestreamResponse>> EndLivestreamAsync(EndLivestreamRequest request)
    {
        var accessToken = await GetAccessTokenAsync(request.ShopId);
        if (accessToken == null) return Result<EndLivestreamResponse>.Fail("Access token not found.");

        var body = new { livestream_id = request.LivestreamId };
        var response = await _apiClient.PostAsync<EndLivestreamResponse>("/api/v2/live/end_live", body, accessToken, request.ShopId);
        if (response == null) return Result<EndLivestreamResponse>.Fail("No response from Shopee API.");
        return Result<EndLivestreamResponse>.Ok(response);
    }

    public async Task<Result<GetLivestreamListResponse>> GetLivestreamListAsync(GetLivestreamListRequest request)
    {
        var accessToken = await GetAccessTokenAsync(request.ShopId);
        if (accessToken == null) return Result<GetLivestreamListResponse>.Fail("Access token not found.");

        var queryParams = new Dictionary<string, string> { ["page_no"] = request.PageNo.ToString(), ["page_size"] = request.PageSize.ToString(), ["status"] = request.Status };
        var response = await _apiClient.GetAsync<GetLivestreamListResponse>("/api/v2/live/get_live_list", queryParams, accessToken, request.ShopId);
        if (response == null) return Result<GetLivestreamListResponse>.Fail("No response from Shopee API.");
        return Result<GetLivestreamListResponse>.Ok(response);
    }

    public async Task<Result<AddLivestreamProductResponse>> AddLivestreamProductAsync(AddLivestreamProductRequest request)
    {
        var accessToken = await GetAccessTokenAsync(request.ShopId);
        if (accessToken == null) return Result<AddLivestreamProductResponse>.Fail("Access token not found.");

        var body = new { livestream_id = request.LivestreamId, item_id_list = request.ItemIdList };
        var response = await _apiClient.PostAsync<AddLivestreamProductResponse>("/api/v2/live/add_live_product", body, accessToken, request.ShopId);
        if (response == null) return Result<AddLivestreamProductResponse>.Fail("No response from Shopee API.");
        return Result<AddLivestreamProductResponse>.Ok(response);
    }

    public async Task<Result<DeleteLivestreamProductResponse>> DeleteLivestreamProductAsync(DeleteLivestreamProductRequest request)
    {
        var accessToken = await GetAccessTokenAsync(request.ShopId);
        if (accessToken == null) return Result<DeleteLivestreamProductResponse>.Fail("Access token not found.");

        var body = new { livestream_id = request.LivestreamId, item_id_list = request.ItemIdList };
        var response = await _apiClient.PostAsync<DeleteLivestreamProductResponse>("/api/v2/live/delete_live_product", body, accessToken, request.ShopId);
        if (response == null) return Result<DeleteLivestreamProductResponse>.Fail("No response from Shopee API.");
        return Result<DeleteLivestreamProductResponse>.Ok(response);
    }

    public async Task<Result<GetLivestreamProductListResponse>> GetLivestreamProductListAsync(GetLivestreamProductListRequest request)
    {
        var accessToken = await GetAccessTokenAsync(request.ShopId);
        if (accessToken == null) return Result<GetLivestreamProductListResponse>.Fail("Access token not found.");

        var queryParams = new Dictionary<string, string> { ["livestream_id"] = request.LivestreamId.ToString() };
        var response = await _apiClient.GetAsync<GetLivestreamProductListResponse>("/api/v2/live/get_live_product_list", queryParams, accessToken, request.ShopId);
        if (response == null) return Result<GetLivestreamProductListResponse>.Fail("No response from Shopee API.");
        return Result<GetLivestreamProductListResponse>.Ok(response);
    }

    public async Task<Result<PinLivestreamProductResponse>> PinLivestreamProductAsync(PinLivestreamProductRequest request)
    {
        var accessToken = await GetAccessTokenAsync(request.ShopId);
        if (accessToken == null) return Result<PinLivestreamProductResponse>.Fail("Access token not found.");

        var body = new { livestream_id = request.LivestreamId, item_id = request.ItemId };
        var response = await _apiClient.PostAsync<PinLivestreamProductResponse>("/api/v2/live/pin_live_product", body, accessToken, request.ShopId);
        if (response == null) return Result<PinLivestreamProductResponse>.Fail("No response from Shopee API.");
        return Result<PinLivestreamProductResponse>.Ok(response);
    }

    public async Task<Result<GetLivestreamMetricsResponse>> GetLivestreamMetricsAsync(GetLivestreamMetricsRequest request)
    {
        var accessToken = await GetAccessTokenAsync(request.ShopId);
        if (accessToken == null) return Result<GetLivestreamMetricsResponse>.Fail("Access token not found.");

        var queryParams = new Dictionary<string, string> { ["livestream_id"] = request.LivestreamId.ToString() };
        var response = await _apiClient.GetAsync<GetLivestreamMetricsResponse>("/api/v2/live/get_live_metrics", queryParams, accessToken, request.ShopId);
        if (response == null) return Result<GetLivestreamMetricsResponse>.Fail("No response from Shopee API.");
        return Result<GetLivestreamMetricsResponse>.Ok(response);
    }

    public async Task<Result<GetLivestreamCommentResponse>> GetLivestreamCommentAsync(GetLivestreamCommentRequest request)
    {
        var accessToken = await GetAccessTokenAsync(request.ShopId);
        if (accessToken == null) return Result<GetLivestreamCommentResponse>.Fail("Access token not found.");

        var queryParams = new Dictionary<string, string> { ["livestream_id"] = request.LivestreamId.ToString(), ["page_no"] = request.PageNo.ToString(), ["page_size"] = request.PageSize.ToString() };
        var response = await _apiClient.GetAsync<GetLivestreamCommentResponse>("/api/v2/live/get_live_comment", queryParams, accessToken, request.ShopId);
        if (response == null) return Result<GetLivestreamCommentResponse>.Fail("No response from Shopee API.");
        return Result<GetLivestreamCommentResponse>.Ok(response);
    }

    public async Task<Result<GetLivestreamReplayListResponse>> GetLivestreamReplayListAsync(GetLivestreamReplayListRequest request)
    {
        var accessToken = await GetAccessTokenAsync(request.ShopId);
        if (accessToken == null) return Result<GetLivestreamReplayListResponse>.Fail("Access token not found.");

        var queryParams = new Dictionary<string, string> { ["livestream_id"] = request.LivestreamId.ToString() };
        var response = await _apiClient.GetAsync<GetLivestreamReplayListResponse>("/api/v2/live/get_live_replay_list", queryParams, accessToken, request.ShopId);
        if (response == null) return Result<GetLivestreamReplayListResponse>.Fail("No response from Shopee API.");
        return Result<GetLivestreamReplayListResponse>.Ok(response);
    }

    public async Task<Result<GetLivestreamCoverResponse>> GetLivestreamCoverAsync(GetLivestreamCoverRequest request)
    {
        var accessToken = await GetAccessTokenAsync(request.ShopId);
        if (accessToken == null) return Result<GetLivestreamCoverResponse>.Fail("Access token not found.");

        var queryParams = new Dictionary<string, string> { ["livestream_id"] = request.LivestreamId.ToString() };
        var response = await _apiClient.GetAsync<GetLivestreamCoverResponse>("/api/v2/live/get_live_cover", queryParams, accessToken, request.ShopId);
        if (response == null) return Result<GetLivestreamCoverResponse>.Fail("No response from Shopee API.");
        return Result<GetLivestreamCoverResponse>.Ok(response);
    }

    public async Task<Result<SendLivestreamNotificationResponse>> SendLivestreamNotificationAsync(SendLivestreamNotificationRequest request)
    {
        var accessToken = await GetAccessTokenAsync(request.ShopId);
        if (accessToken == null) return Result<SendLivestreamNotificationResponse>.Fail("Access token not found.");

        var body = new { livestream_id = request.LivestreamId, notification_type = request.NotificationType, message = request.Message };
        var response = await _apiClient.PostAsync<SendLivestreamNotificationResponse>("/api/v2/live/send_live_notification", body, accessToken, request.ShopId);
        if (response == null) return Result<SendLivestreamNotificationResponse>.Fail("No response from Shopee API.");
        return Result<SendLivestreamNotificationResponse>.Ok(response);
    }

    public async Task<Result<GetLivestreamReportResponse>> GetLivestreamReportAsync(GetLivestreamReportRequest request)
    {
        var accessToken = await GetAccessTokenAsync(request.ShopId);
        if (accessToken == null) return Result<GetLivestreamReportResponse>.Fail("Access token not found.");

        var queryParams = new Dictionary<string, string> { ["start_date"] = request.StartDate, ["end_date"] = request.EndDate, ["page_no"] = request.PageNo.ToString(), ["page_size"] = request.PageSize.ToString() };
        var response = await _apiClient.GetAsync<GetLivestreamReportResponse>("/api/v2/live/get_live_report", queryParams, accessToken, request.ShopId);
        if (response == null) return Result<GetLivestreamReportResponse>.Fail("No response from Shopee API.");
        return Result<GetLivestreamReportResponse>.Ok(response);
    }
}
