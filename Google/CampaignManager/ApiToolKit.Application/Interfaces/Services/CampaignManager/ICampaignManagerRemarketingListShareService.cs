using ApiToolKit.Application.DTOs.CampaignManager.RemarketingListShare;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.CampaignManager;

public interface ICampaignManagerRemarketingListShareService
{
    Task<Result<object>> UpdateRemarketingListShareAsync(UpdateRemarketingListShareRequest request);
    Task<Result<object>> PatchRemarketingListShareAsync(PatchRemarketingListShareRequest request);
    Task<Result<object>> GetRemarketingListShareAsync(GetRemarketingListShareRequest request);
}
