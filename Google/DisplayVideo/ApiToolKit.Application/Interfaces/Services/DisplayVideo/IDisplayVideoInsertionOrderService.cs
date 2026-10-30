using ApiToolKit.Application.DTOs.DisplayVideo.InsertionOrder;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.DisplayVideo;

public interface IDisplayVideoInsertionOrderService
{
    Task<Result<object>> CreateInsertionOrderAsync(CreateInsertionOrderRequest request);
    Task<Result<object>> ListInsertionOrdersAsync(ListInsertionOrdersRequest request);
    Task<Result<object>> UpdateInsertionOrderAsync(UpdateInsertionOrderRequest request);
    Task<Result<object>> GetInsertionOrderAsync(GetInsertionOrderRequest request);
    Task<Result<object>> DeleteInsertionOrderAsync(DeleteInsertionOrderRequest request);
}
