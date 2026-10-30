using ApiToolKit.Application.DTOs.DisplayVideo.LineItem;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.DisplayVideo;

public interface IDisplayVideoLineItemService
{
    Task<Result<object>> BulkListAssignedTargetingOptionsLineItemAsync(BulkListAssignedTargetingOptionsLineItemRequest request);
    Task<Result<object>> BulkEditAssignedTargetingOptionsLineItemAsync(BulkEditAssignedTargetingOptionsLineItemRequest request);
    Task<Result<object>> BulkUpdateLineItemAsync(BulkUpdateLineItemRequest request);
    Task<Result<object>> DeleteLineItemAsync(DeleteLineItemRequest request);
    Task<Result<object>> GetLineItemAsync(GetLineItemRequest request);
    Task<Result<object>> CreateLineItemAsync(CreateLineItemRequest request);
    Task<Result<object>> UpdateLineItemAsync(UpdateLineItemRequest request);
    Task<Result<object>> DuplicateLineItemAsync(DuplicateLineItemRequest request);
    Task<Result<object>> ListLineItemsAsync(ListLineItemsRequest request);
}
