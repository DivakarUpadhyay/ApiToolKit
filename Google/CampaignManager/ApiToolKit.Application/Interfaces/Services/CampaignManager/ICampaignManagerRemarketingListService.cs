using ApiToolKit.Application.DTOs.CampaignManager.RemarketingList;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.CampaignManager;

public interface ICampaignManagerRemarketingListService
{
    Task<Result<object>> ListRemarketingListsAsync(ListRemarketingListsRequest request);
    Task<Result<object>> GetRemarketingListAsync(GetRemarketingListRequest request);
    Task<Result<object>> UpdateRemarketingListAsync(UpdateRemarketingListRequest request);
    Task<Result<object>> UpdateRemarketingListAltAsync(UpdateRemarketingListAltRequest request);
    Task<Result<object>> InsertRemarketingListAsync(InsertRemarketingListRequest request);
}
