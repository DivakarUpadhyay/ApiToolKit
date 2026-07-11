using ApiToolKit.Application.DTOs.GoogleAds.Customer;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.GoogleAds;

public interface IGoogleAdsCustomerService
{
    Task<Result<CustomerResponse>> GetCustomerAsync(GetCustomerRequest request);
    Task<Result<ListAccessibleCustomersResponse>> ListAccessibleCustomersAsync(string accessToken);
}
