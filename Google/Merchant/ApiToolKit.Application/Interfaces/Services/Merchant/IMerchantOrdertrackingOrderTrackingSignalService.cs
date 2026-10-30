using ApiToolKit.Application.DTOs.Merchant.OrdertrackingOrderTrackingSignal;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Merchant;

public interface IMerchantOrdertrackingOrderTrackingSignalService
{
    Task<Result<object>> CreateOrderTrackingSignalAsync(CreateOrderTrackingSignalRequest request);
}
