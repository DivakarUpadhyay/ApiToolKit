using ApiToolKit.Application.DTOs.DisplayVideo.Advertiser;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.DisplayVideo;

public interface IDisplayVideoAdvertiserService
{
    Task<Result<object>> GetAdvertiserAsync(GetAdvertiserRequest request);
    Task<Result<object>> ListAssignedTargetingOptionsAdvertisersAsync(ListAssignedTargetingOptionsAdvertisersRequest request);
    Task<Result<object>> EditAssignedTargetingOptionsAdvertiserAsync(EditAssignedTargetingOptionsAdvertiserRequest request);
    Task<Result<object>> CreateAdvertiserAsync(CreateAdvertiserRequest request);
    Task<Result<object>> UpdateAdvertiserAsync(UpdateAdvertiserRequest request);
    Task<Result<object>> AuditAdvertiserAsync(AuditAdvertiserRequest request);
    Task<Result<object>> ListAdvertisersAsync(ListAdvertisersRequest request);
    Task<Result<object>> DeleteAdvertiserAsync(DeleteAdvertiserRequest request);
}
