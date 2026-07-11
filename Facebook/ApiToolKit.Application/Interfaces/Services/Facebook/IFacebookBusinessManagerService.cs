using ApiToolKit.Application.DTOs.Facebook.BusinessManager;
using ApiToolKit.Domain.Common;
namespace ApiToolKit.Application.Interfaces.Services.Facebook;
public interface IFacebookBusinessManagerService {
    Task<Result<Business>> GetBusinessAsync(string businessId, string accessToken);
    Task<Result<BusinessPagesResponse>> GetOwnedPagesAsync(string businessId, string accessToken);
    Task<Result<BusinessAdAccountsResponse>> GetOwnedAdAccountsAsync(string businessId, string accessToken);
    Task<Result<BusinessInstagramAccountsResponse>> GetOwnedInstagramAccountsAsync(string businessId, string accessToken);
    Task<Result<BusinessCatalogsResponse>> GetOwnedCatalogsAsync(string businessId, string accessToken);
    Task<Result<BusinessUsersResponse>> GetBusinessUsersAsync(string businessId, string accessToken);
    Task<Result<bool>> InviteBusinessUserAsync(string businessId, InviteBusinessUserRequest request, string accessToken);
    Task<Result<bool>> RemoveBusinessUserAsync(string businessId, string userId, string accessToken);
    Task<Result<SystemUsersResponse>> GetSystemUsersAsync(string businessId, string accessToken);
    Task<Result<SystemUser>> CreateSystemUserAsync(string businessId, CreateSystemUserRequest request, string accessToken);
    Task<Result<BusinessPagesResponse>> GetClientPagesAsync(string businessId, string accessToken);
    Task<Result<bool>> ClaimPageAsync(string businessId, ClaimPageRequest request, string accessToken);
}
