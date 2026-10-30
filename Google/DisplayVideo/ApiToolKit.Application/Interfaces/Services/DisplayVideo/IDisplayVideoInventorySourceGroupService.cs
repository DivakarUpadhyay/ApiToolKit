using ApiToolKit.Application.DTOs.DisplayVideo.InventorySourceGroup;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.DisplayVideo;

public interface IDisplayVideoInventorySourceGroupService
{
    Task<Result<object>> GetInventorySourceGroupAsync(GetInventorySourceGroupRequest request);
    Task<Result<object>> DeleteInventorySourceGroupAsync(DeleteInventorySourceGroupRequest request);
    Task<Result<object>> ListInventorySourceGroupsAsync(ListInventorySourceGroupsRequest request);
    Task<Result<object>> UpdateInventorySourceGroupAsync(UpdateInventorySourceGroupRequest request);
    Task<Result<object>> CreateInventorySourceGroupAsync(CreateInventorySourceGroupRequest request);
}
