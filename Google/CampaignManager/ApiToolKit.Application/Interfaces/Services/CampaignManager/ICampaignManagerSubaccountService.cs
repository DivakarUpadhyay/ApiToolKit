using ApiToolKit.Application.DTOs.CampaignManager.Subaccount;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.CampaignManager;

public interface ICampaignManagerSubaccountService
{
    Task<Result<object>> UpdateSubaccountAsync(UpdateSubaccountRequest request);
    Task<Result<object>> PatchSubaccountAsync(PatchSubaccountRequest request);
    Task<Result<object>> InsertSubaccountAsync(InsertSubaccountRequest request);
    Task<Result<object>> GetSubaccountAsync(GetSubaccountRequest request);
    Task<Result<object>> ListSubaccountsAsync(ListSubaccountsRequest request);
}
