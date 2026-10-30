using ApiToolKit.Application.DTOs.Merchant.Issue;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Merchant;

public interface IMerchantIssueService
{
    Task<Result<object>> ListIssuesAsync(ListIssuesRequest request);
}
