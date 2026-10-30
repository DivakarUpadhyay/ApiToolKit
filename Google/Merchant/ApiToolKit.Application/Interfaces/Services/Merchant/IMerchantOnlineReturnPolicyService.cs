using ApiToolKit.Application.DTOs.Merchant.OnlineReturnPolicy;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Merchant;

public interface IMerchantOnlineReturnPolicyService
{
    Task<Result<object>> ListOnlineReturnPoliciesAsync(ListOnlineReturnPoliciesRequest request);
    Task<Result<object>> CreateOnlineReturnPolicyAsync(CreateOnlineReturnPolicyRequest request);
    Task<Result<object>> GetOnlineReturnPolicyAsync(GetOnlineReturnPolicyRequest request);
    Task<Result<object>> DeleteOnlineReturnPolicyAsync(DeleteOnlineReturnPolicyRequest request);
}
