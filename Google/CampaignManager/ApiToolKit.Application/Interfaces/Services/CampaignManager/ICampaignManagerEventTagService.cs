using ApiToolKit.Application.DTOs.CampaignManager.EventTag;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.CampaignManager;

public interface ICampaignManagerEventTagService
{
    Task<Result<object>> ListEventTagsAsync(ListEventTagsRequest request);
    Task<Result<object>> UpdateEventTagAsync(UpdateEventTagRequest request);
    Task<Result<object>> PatchEventTagAsync(PatchEventTagRequest request);
    Task<Result<object>> DeleteEventTagAsync(DeleteEventTagRequest request);
    Task<Result<object>> GetEventTagAsync(GetEventTagRequest request);
    Task<Result<object>> InsertEventTagAsync(InsertEventTagRequest request);
}
