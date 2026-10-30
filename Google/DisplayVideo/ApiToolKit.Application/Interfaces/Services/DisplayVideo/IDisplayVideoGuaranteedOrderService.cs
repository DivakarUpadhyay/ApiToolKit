using ApiToolKit.Application.DTOs.DisplayVideo.GuaranteedOrder;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.DisplayVideo;

public interface IDisplayVideoGuaranteedOrderService
{
    Task<Result<object>> GetGuaranteedOrderAsync(GetGuaranteedOrderRequest request);
    Task<Result<object>> EditGuaranteedOrderReadAccessorsAsync(EditGuaranteedOrderReadAccessorsRequest request);
    Task<Result<object>> UpdateGuaranteedOrderAsync(UpdateGuaranteedOrderRequest request);
    Task<Result<object>> ListGuaranteedOrdersAsync(ListGuaranteedOrdersRequest request);
    Task<Result<object>> CreateGuaranteedOrderAsync(CreateGuaranteedOrderRequest request);
}
