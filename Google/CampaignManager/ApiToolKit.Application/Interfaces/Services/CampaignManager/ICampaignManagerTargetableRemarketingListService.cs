using ApiToolKit.Application.DTOs.CampaignManager.TargetableRemarketingList;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.CampaignManager;

public interface ICampaignManagerTargetableRemarketingListService
{
    Task<Result<object>> ListTargetableRemarketingListsAsync(ListTargetableRemarketingListsRequest request);
    Task<Result<object>> GetTargetableRemarketingListAsync(GetTargetableRemarketingListRequest request);
}
