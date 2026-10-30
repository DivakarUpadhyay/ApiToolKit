using ApiToolKit.Application.DTOs.TagManager.Trigger;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.TagManager;

public interface ITagManagerTriggerService
{
    Task<Result<object>> ListTriggersAsync(ListTriggersRequest request);
    Task<Result<object>> GetTriggerAsync(GetTriggerRequest request);
    Task<Result<object>> CreateTriggerAsync(CreateTriggerRequest request);
    Task<Result<object>> UpdateTriggerAsync(UpdateTriggerRequest request);
    Task<Result<object>> DeleteTriggerAsync(DeleteTriggerRequest request);
    Task<Result<object>> RevertTriggerAsync(RevertTriggerRequest request);
}
