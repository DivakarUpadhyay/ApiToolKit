using ApiToolKit.Application.DTOs.Facebook.Video;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Facebook;

public interface IFacebookVideoService
{
    Task<Result<UploadVideoResponse>> UploadVideoAsync(UploadVideoRequest request, string accessToken);
    Task<Result<VideoResponse>> GetVideoAsync(string videoId, string accessToken);
    Task<Result<GetVideosResponse>> GetVideosAsync(GetVideosRequest request, string accessToken);
    Task<Result<bool>> UpdateVideoAsync(string videoId, string? title, string? description, string accessToken);
    Task<Result<bool>> DeleteVideoAsync(string videoId, string accessToken);
    Task<Result<VideoStatusResponse>> GetVideoStatusAsync(string videoId, string accessToken);
    Task<Result<object>> GetVideoThumbnailsAsync(string videoId, string accessToken);
}
