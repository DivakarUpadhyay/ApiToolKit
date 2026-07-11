using ApiToolKit.Application.DTOs.Shopee.Video;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Shopee;

public interface IShopeeVideoService
{
    Task<Result<UploadVideoResponse>> UploadVideoAsync(UploadVideoRequest request);
    Task<Result<GetVideoListResponse>> GetVideoListAsync(GetVideoListRequest request);
    Task<Result<GetVideoResponse>> GetVideoAsync(GetVideoRequest request);
    Task<Result<DeleteVideoResponse>> DeleteVideoAsync(DeleteVideoRequest request);
    Task<Result<UpdateVideoResponse>> UpdateVideoAsync(UpdateVideoRequest request);
    Task<Result<GetVideoUploadStatusResponse>> GetVideoUploadStatusAsync(GetVideoUploadStatusRequest request);
    Task<Result<AddVideoToItemResponse>> AddVideoToItemAsync(AddVideoToItemRequest request);
    Task<Result<DeleteVideoFromItemResponse>> DeleteVideoFromItemAsync(DeleteVideoFromItemRequest request);
    Task<Result<GetItemVideoListResponse>> GetItemVideoListAsync(GetItemVideoListRequest request);
}
