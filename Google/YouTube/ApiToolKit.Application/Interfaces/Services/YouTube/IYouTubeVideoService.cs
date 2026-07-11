using ApiToolKit.Application.DTOs.YouTube.Video;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.YouTube;

public interface IYouTubeVideoService
{
    Task<Result<VideoListResponse>> ListVideosAsync(ListVideosRequest request, string accessToken);
    Task<Result<VideoResponse>> GetVideoAsync(GetVideoRequest request, string accessToken);
    Task<Result<VideoResponse>> UpdateVideoAsync(UpdateVideoRequest request, string accessToken);
    Task<Result<bool>> DeleteVideoAsync(DeleteVideoRequest request, string accessToken);
    Task<Result<bool>> RateVideoAsync(RateVideoRequest request, string accessToken);
    Task<Result<UploadVideoResult>> UploadVideoAsync(UploadVideoMetadata metadata, Stream videoStream, string fileName, string mimeType, string accessToken);
    Task<Result<BulkUploadResult>> BulkUploadVideosAsync(List<(UploadVideoMetadata Metadata, Stream Stream, string FileName, string MimeType)> uploads, string accessToken);
}
