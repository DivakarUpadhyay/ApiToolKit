using ApiToolKit.Application.DTOs.Facebook.Photo;
using ApiToolKit.Application.Interfaces.Services.Facebook;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Entities.Facebook;
using ApiToolKit.Domain.Interfaces.Repositories.Facebook;
using Newtonsoft.Json;

namespace ApiToolKit.Application.Services.Facebook;

public class FacebookPhotoService : IFacebookPhotoService
{
    private readonly IFacebookAuthRepository _authRepo;
    private readonly IFacebookPhotoRepository _photoRepo;
    private readonly IFacebookApiClient _apiClient;

    public FacebookPhotoService(IFacebookAuthRepository authRepo, IFacebookPhotoRepository photoRepo, IFacebookApiClient apiClient)
    {
        _authRepo = authRepo;
        _photoRepo = photoRepo;
        _apiClient = apiClient;
    }

    public async Task<Result<UploadPhotoResponse>> UploadPhotoAsync(UploadPhotoRequest request, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<UploadPhotoResponse>.Fail("Facebook configuration not found.");

        var body = new Dictionary<string, object?>();
        if (request.Url != null) body["url"] = request.Url;
        if (request.Caption != null) body["caption"] = request.Caption;
        body["published"] = request.Published;
        if (request.AlbumId != null) body["album_id"] = request.AlbumId;

        var response = await _apiClient.PostAsync<dynamic>($"/{request.PageId}/photos", body, accessToken);
        if (response == null) return Result<UploadPhotoResponse>.Fail("No response from Facebook API.");

        string photoId = response?.id?.ToString() ?? string.Empty;
        string postId = response?.post_id?.ToString() ?? string.Empty;

        await _photoRepo.SavePhotoAsync(new FacebookPhoto
        {
            PhotoId = photoId,
            PageId = request.PageId,
            Name = request.Caption,
            Source = request.Url,
            AlbumId = request.AlbumId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        });

        await _photoRepo.SaveApiLogAsync(new FacebookApiLog
        {
            PageId = request.PageId,
            ApiName = "UploadPhoto",
            RequestBody = JsonConvert.SerializeObject(request),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = true,
            StatusCode = 200,
            CreatedAt = DateTime.UtcNow
        });

        return Result<UploadPhotoResponse>.Ok(new UploadPhotoResponse { Id = photoId, PostId = postId });
    }

    public async Task<Result<PhotoResponse>> GetPhotoAsync(string photoId, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<PhotoResponse>.Fail("Facebook configuration not found.");

        var queryParams = new Dictionary<string, string>
        {
            ["fields"] = "id,name,source,link,width,height,created_time,images",
            ["access_token"] = accessToken
        };

        var response = await _apiClient.GetAsync<dynamic>($"/{photoId}", queryParams);
        if (response == null) return Result<PhotoResponse>.Fail("No response from Facebook API.");

        var photo = JsonConvert.DeserializeObject<PhotoResponse>(JsonConvert.SerializeObject(response)) ?? new PhotoResponse();
        return Result<PhotoResponse>.Ok(photo);
    }

    public async Task<Result<GetPhotosResponse>> GetPhotosAsync(GetPhotosRequest request, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<GetPhotosResponse>.Fail("Facebook configuration not found.");

        var queryParams = new Dictionary<string, string>
        {
            ["fields"] = request.Fields,
            ["limit"] = request.Limit.ToString(),
            ["access_token"] = accessToken
        };
        if (request.After != null) queryParams["after"] = request.After;

        var endpoint = request.AlbumId != null
            ? $"/{request.AlbumId}/photos"
            : $"/{request.PageId}/photos";

        var response = await _apiClient.GetAsync<dynamic>(endpoint, queryParams);
        if (response == null) return Result<GetPhotosResponse>.Fail("No response from Facebook API.");

        var photos = JsonConvert.DeserializeObject<List<PhotoResponse>>(
            JsonConvert.SerializeObject(response?.data ?? new List<object>())) ?? new List<PhotoResponse>();

        return Result<GetPhotosResponse>.Ok(new GetPhotosResponse { Data = photos });
    }

    public async Task<Result<bool>> DeletePhotoAsync(string photoId, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<bool>.Fail("Facebook configuration not found.");

        var response = await _apiClient.DeleteAsync<dynamic>($"/{photoId}", accessToken);
        if (response == null) return Result<bool>.Fail("No response from Facebook API.");

        await _photoRepo.DeletePhotoAsync(photoId);
        return Result<bool>.Ok(true);
    }

    public async Task<Result<object>> GetAlbumsAsync(string pageId, string accessToken, int limit)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<object>.Fail("Facebook configuration not found.");

        var queryParams = new Dictionary<string, string>
        {
            ["fields"] = "id,name,description,created_time,count,cover_photo",
            ["limit"] = limit.ToString(),
            ["access_token"] = accessToken
        };

        var response = await _apiClient.GetAsync<object>($"/{pageId}/albums", queryParams);
        if (response == null) return Result<object>.Fail("No response from Facebook API.");

        return Result<object>.Ok(response);
    }

    public async Task<Result<CreateAlbumResponse>> CreateAlbumAsync(CreateAlbumRequest request, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<CreateAlbumResponse>.Fail("Facebook configuration not found.");

        var body = new Dictionary<string, object?> { ["name"] = request.Name };
        if (request.Message != null) body["message"] = request.Message;
        if (request.Location != null) body["location"] = request.Location;

        var response = await _apiClient.PostAsync<dynamic>($"/{request.PageId}/albums", body, accessToken);
        if (response == null) return Result<CreateAlbumResponse>.Fail("No response from Facebook API.");

        return Result<CreateAlbumResponse>.Ok(new CreateAlbumResponse { Id = response?.id?.ToString() ?? string.Empty });
    }

    public async Task<Result<GetPhotosResponse>> GetAlbumPhotosAsync(string albumId, string accessToken, int limit)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<GetPhotosResponse>.Fail("Facebook configuration not found.");

        var queryParams = new Dictionary<string, string>
        {
            ["fields"] = "id,name,source,link,width,height,created_time",
            ["limit"] = limit.ToString(),
            ["access_token"] = accessToken
        };

        var response = await _apiClient.GetAsync<dynamic>($"/{albumId}/photos", queryParams);
        if (response == null) return Result<GetPhotosResponse>.Fail("No response from Facebook API.");

        var photos = JsonConvert.DeserializeObject<List<PhotoResponse>>(
            JsonConvert.SerializeObject(response?.data ?? new List<object>())) ?? new List<PhotoResponse>();

        return Result<GetPhotosResponse>.Ok(new GetPhotosResponse { Data = photos });
    }
}
