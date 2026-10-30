using ApiToolKit.Application.DTOs.BusinessProfile.Account;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.BusinessProfile;

public interface IBusinessProfileAccountService
{
    Task<Result<object>> ListAccountsAsync(ListAccountsRequest request);
    Task<Result<object>> GetAccountAsync(GetAccountRequest request);
    Task<Result<object>> CreateAccountAsync(CreateAccountRequest request);
    Task<Result<object>> UpdateAccountAsync(UpdateAccountRequest request);
    Task<Result<object>> ListAccountAdminsAsync(ListAccountAdminsRequest request);
    Task<Result<object>> InviteAccountAdminAsync(InviteAccountAdminRequest request);
    Task<Result<object>> UpdateAccountAdminAsync(UpdateAccountAdminRequest request);
    Task<Result<object>> RemoveAccountAdminAsync(RemoveAccountAdminRequest request);
    Task<Result<object>> ListInvitationsAsync(ListInvitationsRequest request);
    Task<Result<object>> AcceptInvitationAsync(AcceptInvitationRequest request);
    Task<Result<object>> DeclineInvitationAsync(DeclineInvitationRequest request);
    Task<Result<object>> GetNotificationSettingAsync(GetNotificationSettingRequest request);
    Task<Result<object>> UpdateNotificationSettingAsync(UpdateNotificationSettingRequest request);
}
