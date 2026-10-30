using ApiToolKit.Application.DTOs.DisplayVideo.InventorySourceGroupAssignedInventorySource;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.DisplayVideo;

public interface IDisplayVideoInventorySourceGroupAssignedInventorySourceService
{
    Task<Result<object>> ListAssignedInventorySourcesAsync(ListAssignedInventorySourcesRequest request);
    Task<Result<object>> BulkEditAssignedInventorySourceAsync(BulkEditAssignedInventorySourceRequest request);
    Task<Result<object>> CreateAssignedInventorySourceAsync(CreateAssignedInventorySourceRequest request);
    Task<Result<object>> DeleteAssignedInventorySourceAsync(DeleteAssignedInventorySourceRequest request);
}
