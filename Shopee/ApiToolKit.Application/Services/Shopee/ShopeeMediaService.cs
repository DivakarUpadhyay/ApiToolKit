using ApiToolKit.Application.DTOs.Shopee.Media;
using ApiToolKit.Application.Interfaces.Services.Shopee;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.Shopee;

namespace ApiToolKit.Application.Services.Shopee;

public class ShopeeMediaService : IShopeeMediaService
{
    private readonly IShopeeAuthRepository _authRepo;
    private readonly IShopeeApiClient _apiClient;

    public ShopeeMediaService(IShopeeAuthRepository authRepo, IShopeeApiClient apiClient)
    {
        _authRepo = authRepo;
        _apiClient = apiClient;
    }

    private async Task<string?> GetAccessTokenAsync(long shopId)
    {
        var token = await _authRepo.GetTokenByShopIdAsync(shopId);
        return token?.AccessToken;
    }

    public async Task<Result<UploadImageResponse>> UploadImageAsync(UploadImageRequest request)
    {
        var accessToken = await GetAccessTokenAsync(request.ShopId);
        if (accessToken == null) return Result<UploadImageResponse>.Fail("Access token not found.");

        var content = new MultipartFormDataContent();
        content.Add(new ByteArrayContent(request.ImageBytes), "image", request.FileName);

        var response = await _apiClient.PostMultipartAsync<UploadImageResponse>("/api/v2/media_space/upload_image", content, accessToken, request.ShopId);
        if (response == null) return Result<UploadImageResponse>.Fail("No response from Shopee API.");
        return Result<UploadImageResponse>.Ok(response);
    }

    public async Task<Result<UploadImageResponse>> UploadImageByUrlAsync(UploadImageByUrlRequest request)
    {
        var accessToken = await GetAccessTokenAsync(request.ShopId);
        if (accessToken == null) return Result<UploadImageResponse>.Fail("Access token not found.");

        var body = new { image_url = request.ImageUrl };
        var response = await _apiClient.PostAsync<UploadImageResponse>("/api/v2/media_space/upload_image_by_url", body, accessToken, request.ShopId);
        if (response == null) return Result<UploadImageResponse>.Fail("No response from Shopee API.");
        return Result<UploadImageResponse>.Ok(response);
    }

    public async Task<Result<UploadVideoResponse>> UploadVideoAsync(UploadVideoRequest request)
    {
        var accessToken = await GetAccessTokenAsync(request.ShopId);
        if (accessToken == null) return Result<UploadVideoResponse>.Fail("Access token not found.");

        var body = new { video_upload_id = request.VideoUploadId, video_content = request.VideoContent, part_seq = request.PartSeq };
        var response = await _apiClient.PostAsync<UploadVideoResponse>("/api/v2/media_space/upload_video_part", body, accessToken, request.ShopId);
        if (response == null) return Result<UploadVideoResponse>.Fail("No response from Shopee API.");
        return Result<UploadVideoResponse>.Ok(response);
    }
}


