using ApiToolKit.Application.DTOs.CampaignManager.StudioCreative;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.CampaignManager;

public interface ICampaignManagerStudioCreativeService
{
    Task<Result<object>> GetStudioCreativeAsync(GetStudioCreativeRequest request);
    Task<Result<object>> InsertStudioCreativeAsync(InsertStudioCreativeRequest request);
    Task<Result<object>> PublishStudioCreativeAsync(PublishStudioCreativeRequest request);
}
