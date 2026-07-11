using ApiToolKit.Application.DTOs.Shopee.Livestream;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Shopee;

public interface IShopeeLivestreamService
{
    Task<Result<CreateLivestreamResponse>> CreateLivestreamAsync(CreateLivestreamRequest request);
    Task<Result<GetLivestreamDetailResponse>> GetLivestreamDetailAsync(GetLivestreamDetailRequest request);
    Task<Result<UpdateLivestreamResponse>> UpdateLivestreamAsync(UpdateLivestreamRequest request);
    Task<Result<EndLivestreamResponse>> EndLivestreamAsync(EndLivestreamRequest request);
    Task<Result<GetLivestreamListResponse>> GetLivestreamListAsync(GetLivestreamListRequest request);
    Task<Result<AddLivestreamProductResponse>> AddLivestreamProductAsync(AddLivestreamProductRequest request);
    Task<Result<DeleteLivestreamProductResponse>> DeleteLivestreamProductAsync(DeleteLivestreamProductRequest request);
    Task<Result<GetLivestreamProductListResponse>> GetLivestreamProductListAsync(GetLivestreamProductListRequest request);
    Task<Result<PinLivestreamProductResponse>> PinLivestreamProductAsync(PinLivestreamProductRequest request);
    Task<Result<GetLivestreamMetricsResponse>> GetLivestreamMetricsAsync(GetLivestreamMetricsRequest request);
    Task<Result<GetLivestreamCommentResponse>> GetLivestreamCommentAsync(GetLivestreamCommentRequest request);
    Task<Result<GetLivestreamReplayListResponse>> GetLivestreamReplayListAsync(GetLivestreamReplayListRequest request);
    Task<Result<GetLivestreamCoverResponse>> GetLivestreamCoverAsync(GetLivestreamCoverRequest request);
    Task<Result<SendLivestreamNotificationResponse>> SendLivestreamNotificationAsync(SendLivestreamNotificationRequest request);
    Task<Result<GetLivestreamReportResponse>> GetLivestreamReportAsync(GetLivestreamReportRequest request);
}
