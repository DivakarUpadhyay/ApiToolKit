using ApiToolKit.Application.DTOs.Merchant.IssueresolutionAggregateProductStatuse;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Merchant;

public interface IMerchantIssueresolutionAggregateProductStatuseService
{
    Task<Result<object>> ListAggregateProductStatusesAsync(ListAggregateProductStatusesRequest request);
}
