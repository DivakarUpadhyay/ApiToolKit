using ApiToolKit.Application.DTOs.DisplayVideo.InventorySource;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.DisplayVideo;

public interface IDisplayVideoInventorySourceService
{
    Task<Result<object>> UpdateInventorySourceAsync(UpdateInventorySourceRequest request);
    Task<Result<object>> GetInventorySourceAsync(GetInventorySourceRequest request);
    Task<Result<object>> CreateInventorySourceAsync(CreateInventorySourceRequest request);
    Task<Result<object>> EditInventorySourceReadWriteAccessorsAsync(EditInventorySourceReadWriteAccessorsRequest request);
    Task<Result<object>> ListInventorySourcesAsync(ListInventorySourcesRequest request);
}
