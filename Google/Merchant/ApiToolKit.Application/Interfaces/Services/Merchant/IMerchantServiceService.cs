using ApiToolKit.Application.DTOs.Merchant.Service;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Merchant;

public interface IMerchantServiceService
{
    Task<Result<object>> RejectServiceAsync(RejectServiceRequest request);
    Task<Result<object>> ApproveServiceAsync(ApproveServiceRequest request);
    Task<Result<object>> ProposeServiceAsync(ProposeServiceRequest request);
    Task<Result<object>> GetServiceAsync(GetServiceRequest request);
    Task<Result<object>> ListServicesAsync(ListServicesRequest request);
}
