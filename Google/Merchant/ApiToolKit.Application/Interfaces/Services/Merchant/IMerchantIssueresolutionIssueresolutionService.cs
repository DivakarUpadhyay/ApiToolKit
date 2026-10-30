using ApiToolKit.Application.DTOs.Merchant.IssueresolutionIssueresolution;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Merchant;

public interface IMerchantIssueresolutionIssueresolutionService
{
    Task<Result<object>> TriggeractionIssueresolutionAsync(TriggeractionIssueresolutionRequest request);
    Task<Result<object>> RenderaccountissuesIssueresolutionAsync(RenderaccountissuesIssueresolutionRequest request);
    Task<Result<object>> RenderproductissuesIssueresolutionAsync(RenderproductissuesIssueresolutionRequest request);
}
