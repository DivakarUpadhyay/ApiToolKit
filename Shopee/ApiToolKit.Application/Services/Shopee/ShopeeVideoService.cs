using ApiToolKit.Application.DTOs.Shopee.Video;
using ApiToolKit.Application.Interfaces.Services.Shopee;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.Shopee;

namespace ApiToolKit.Application.Services.Shopee;

public class ShopeeVideoService : IShopeeVideoService
{
    private readonly IShopeeAuthRepository _authRepo;
    private readonly IShopeeApiClient _apiClient;

    public ShopeeVideoService(IShopeeAuthRepository authRepo, IShopeeApiClient apiClient)
    {
        _authRepo = authRepo;
        _apiClient = apiClient;
    }

    private async Task<string?> GetAccessTokenAsync(long shopId)
    {
        var token = await _authRepo.GetTokenByShopIdAsync(shopId);
        return token?.AccessToken;
    }

    public async Task<Result<UploadVideoResponse>> UploadVideoAsync(UploadVideoRequest request)
    {
        var accessToken = await GetAccessTokenAsync(request.ShopId);
        if (accessToken == null) return Result<UploadVideoResponse>.Fail("Access token not found.");

        var body = new { video_data = Convert.ToBase64String(request.VideoData), file_name = request.FileName };
        var response = await _apiClient.PostAsync<UploadVideoResponse>("/api/v2/media_space/upload_video", body, accessToken, request.ShopId);
        if (response == null) return Result<UploadVideoResponse>.Fail("No response from Shopee API.");
        return Result<UploadVideoResponse>.Ok(response);
    }

    public async Task<Result<GetVideoListResponse>> GetVideoListAsync(GetVideoListRequest request)
    {
        var accessToken = await GetAccessTokenAsync(request.ShopId);
        if (accessToken == null) return Result<GetVideoListResponse>.Fail("Access token not found.");

        var queryParams = new Dictionary<string, string> { ["page_no"] = request.PageNo.ToString(), ["page_size"] = request.PageSize.ToString(), ["status"] = request.Status };
        var response = await _apiClient.GetAsync<GetVideoListResponse>("/api/v2/media_space/get_video_list", queryParams, accessToken, request.ShopId);
        if (response == null) return Result<GetVideoListResponse>.Fail("No response from Shopee API.");
        return Result<GetVideoListResponse>.Ok(response);
    }

    public async Task<Result<GetVideoResponse>> GetVideoAsync(GetVideoRequest request)
    {
        var accessToken = await GetAccessTokenAsync(request.ShopId);
        if (accessToken == null) return Result<GetVideoResponse>.Fail("Access token not found.");

        var queryParams = new Dictionary<string, string> { ["video_id"] = request.VideoId };
        var response = await _apiClient.GetAsync<GetVideoResponse>("/api/v2/media_space/get_video", queryParams, accessToken, request.ShopId);
        if (response == null) return Result<GetVideoResponse>.Fail("No response from Shopee API.");
        return Result<GetVideoResponse>.Ok(response);
    }

    public async Task<Result<DeleteVideoResponse>> DeleteVideoAsync(DeleteVideoRequest request)
    {
        var accessToken = await GetAccessTokenAsync(request.ShopId);
        if (accessToken == null) return Result<DeleteVideoResponse>.Fail("Access token not found.");

        var body = new { video_id_list = request.VideoIdList };
        var response = await _apiClient.PostAsync<DeleteVideoResponse>("/api/v2/media_space/delete_video", body, accessToken, request.ShopId);
        if (response == null) return Result<DeleteVideoResponse>.Fail("No response from Shopee API.");
        return Result<DeleteVideoResponse>.Ok(response);
    }

    public async Task<Result<UpdateVideoResponse>> UpdateVideoAsync(UpdateVideoRequest request)
    {
        var accessToken = await GetAccessTokenAsync(request.ShopId);
        if (accessToken == null) return Result<UpdateVideoResponse>.Fail("Access token not found.");

        var body = new { video_id = request.VideoId, title = request.Title };
        var response = await _apiClient.PostAsync<UpdateVideoResponse>("/api/v2/media_space/update_video", body, accessToken, request.ShopId);
        if (response == null) return Result<UpdateVideoResponse>.Fail("No response from Shopee API.");
        return Result<UpdateVideoResponse>.Ok(response);
    }

    public async Task<Result<GetVideoUploadStatusResponse>> GetVideoUploadStatusAsync(GetVideoUploadStatusRequest request)
    {
        var accessToken = await GetAccessTokenAsync(request.ShopId);
        if (accessToken == null) return Result<GetVideoUploadStatusResponse>.Fail("Access token not found.");

        var queryParams = new Dictionary<string, string> { ["video_id"] = request.VideoId };
        var response = await _apiClient.GetAsync<GetVideoUploadStatusResponse>("/api/v2/media_space/get_video_upload_status", queryParams, accessToken, request.ShopId);
        if (response == null) return Result<GetVideoUploadStatusResponse>.Fail("No response from Shopee API.");
        return Result<GetVideoUploadStatusResponse>.Ok(response);
    }

    public async Task<Result<AddVideoToItemResponse>> AddVideoToItemAsync(AddVideoToItemRequest request)
    {
        var accessToken = await GetAccessTokenAsync(request.ShopId);
        if (accessToken == null) return Result<AddVideoToItemResponse>.Fail("Access token not found.");

        var body = new { item_id = request.ItemId, video_id = request.VideoId };
        var response = await _apiClient.PostAsync<AddVideoToItemResponse>("/api/v2/media_space/add_video_to_item", body, accessToken, request.ShopId);
        if (response == null) return Result<AddVideoToItemResponse>.Fail("No response from Shopee API.");
        return Result<AddVideoToItemResponse>.Ok(response);
    }

    public async Task<Result<DeleteVideoFromItemResponse>> DeleteVideoFromItemAsync(DeleteVideoFromItemRequest request)
    {
        var accessToken = await GetAccessTokenAsync(request.ShopId);
        if (accessToken == null) return Result<DeleteVideoFromItemResponse>.Fail("Access token not found.");

        var body = new { item_id = request.ItemId, video_id = request.VideoId };
        var response = await _apiClient.PostAsync<DeleteVideoFromItemResponse>("/api/v2/media_space/delete_video_from_item", body, accessToken, request.ShopId);
        if (response == null) return Result<DeleteVideoFromItemResponse>.Fail("No response from Shopee API.");
        return Result<DeleteVideoFromItemResponse>.Ok(response);
    }

    public async Task<Result<GetItemVideoListResponse>> GetItemVideoListAsync(GetItemVideoListRequest request)
    {
        var accessToken = await GetAccessTokenAsync(request.ShopId);
        if (accessToken == null) return Result<GetItemVideoListResponse>.Fail("Access token not found.");

        var queryParams = new Dictionary<string, string> { ["item_id"] = request.ItemId.ToString() };
        var response = await _apiClient.GetAsync<GetItemVideoListResponse>("/api/v2/media_space/get_item_video_list", queryParams, accessToken, request.ShopId);
        if (response == null) return Result<GetItemVideoListResponse>.Fail("No response from Shopee API.");
        return Result<GetItemVideoListResponse>.Ok(response);
    }
}
