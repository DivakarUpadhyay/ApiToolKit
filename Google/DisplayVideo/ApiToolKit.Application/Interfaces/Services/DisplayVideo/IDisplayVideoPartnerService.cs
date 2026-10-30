using ApiToolKit.Application.DTOs.DisplayVideo.Partner;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.DisplayVideo;

public interface IDisplayVideoPartnerService
{
    Task<Result<object>> EditAssignedTargetingOptionsPartnerAsync(EditAssignedTargetingOptionsPartnerRequest request);
    Task<Result<object>> ListPartnersAsync(ListPartnersRequest request);
    Task<Result<object>> GetPartnerAsync(GetPartnerRequest request);
}
