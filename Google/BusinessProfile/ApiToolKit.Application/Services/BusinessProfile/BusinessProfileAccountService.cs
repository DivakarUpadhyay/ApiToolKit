using ApiToolKit.Application.DTOs.BusinessProfile.Account;
using ApiToolKit.Application.Interfaces.Services.BusinessProfile;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.BusinessProfile;

namespace ApiToolKit.Application.Services.BusinessProfile;

public class BusinessProfileAccountService : BusinessProfileServiceBase, IBusinessProfileAccountService
{
    public BusinessProfileAccountService(IBusinessProfileApiClient apiClient, IBusinessProfileAuthService authService, IBusinessProfileAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> ListAccountsAsync(ListAccountsRequest r)
    {
        var url = "https://mybusinessaccountmanagement.googleapis.com/v1/accounts";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "pageSize", r.PageSize?.ToString());
        Add(query, "pageToken", r.PageToken);
        return await SendAsync(r.AccountKey, "ListAccounts", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> GetAccountAsync(GetAccountRequest r)
    {
        var url = $"https://mybusinessaccountmanagement.googleapis.com/v1/accounts/{r.AccountId}";
        return await SendAsync(r.AccountKey, "GetAccount", HttpMethod.Get, url);
    }

    public async Task<Result<object>> CreateAccountAsync(CreateAccountRequest r)
    {
        var url = "https://mybusinessaccountmanagement.googleapis.com/v1/accounts";
        return await SendAsync(r.AccountKey, "CreateAccount", HttpMethod.Post, url, null, new { accountName = r.AccountName, type = r.Type, primaryOwner = r.PrimaryOwner });
    }

    public async Task<Result<object>> UpdateAccountAsync(UpdateAccountRequest r)
    {
        var url = $"https://mybusinessaccountmanagement.googleapis.com/v1/accounts/{r.AccountId}";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "updateMask", r.UpdateMask);
        Add(query, "validateOnly", r.ValidateOnly?.ToString().ToLower());
        return await SendAsync(r.AccountKey, "UpdateAccount", HttpMethod.Patch, url, query, new { accountName = r.AccountName });
    }

    public async Task<Result<object>> ListAccountAdminsAsync(ListAccountAdminsRequest r)
    {
        var url = $"https://mybusinessaccountmanagement.googleapis.com/v1/accounts/{r.AccountId}/admins";
        return await SendAsync(r.AccountKey, "ListAccountAdmins", HttpMethod.Get, url);
    }

    public async Task<Result<object>> InviteAccountAdminAsync(InviteAccountAdminRequest r)
    {
        var url = $"https://mybusinessaccountmanagement.googleapis.com/v1/accounts/{r.AccountId}/admins";
        return await SendAsync(r.AccountKey, "InviteAccountAdmin", HttpMethod.Post, url, null, new { admin = r.Admin, role = r.Role });
    }

    public async Task<Result<object>> UpdateAccountAdminAsync(UpdateAccountAdminRequest r)
    {
        var url = $"https://mybusinessaccountmanagement.googleapis.com/v1/accounts/{r.AccountId}/admins/{r.AdminId}";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "updateMask", "role");
        return await SendAsync(r.AccountKey, "UpdateAccountAdmin", HttpMethod.Patch, url, query, new { role = r.Role });
    }

    public async Task<Result<object>> RemoveAccountAdminAsync(RemoveAccountAdminRequest r)
    {
        var url = $"https://mybusinessaccountmanagement.googleapis.com/v1/accounts/{r.AccountId}/admins/{r.AdminId}";
        return await SendAsync(r.AccountKey, "RemoveAccountAdmin", HttpMethod.Delete, url);
    }

    public async Task<Result<object>> ListInvitationsAsync(ListInvitationsRequest r)
    {
        var url = $"https://mybusinessaccountmanagement.googleapis.com/v1/accounts/{r.AccountId}/invitations";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "filter", r.Filter);
        return await SendAsync(r.AccountKey, "ListInvitations", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> AcceptInvitationAsync(AcceptInvitationRequest r)
    {
        var url = $"https://mybusinessaccountmanagement.googleapis.com/v1/accounts/{r.AccountId}/invitations/{r.InvitationId}:accept";
        return await SendAsync(r.AccountKey, "AcceptInvitation", HttpMethod.Post, url, null, new { });
    }

    public async Task<Result<object>> DeclineInvitationAsync(DeclineInvitationRequest r)
    {
        var url = $"https://mybusinessaccountmanagement.googleapis.com/v1/accounts/{r.AccountId}/invitations/{r.InvitationId}:decline";
        return await SendAsync(r.AccountKey, "DeclineInvitation", HttpMethod.Post, url, null, new { });
    }

    public async Task<Result<object>> GetNotificationSettingAsync(GetNotificationSettingRequest r)
    {
        var url = $"https://mybusinessnotifications.googleapis.com/v1/accounts/{r.AccountId}/notificationSetting";
        return await SendAsync(r.AccountKey, "GetNotificationSetting", HttpMethod.Get, url);
    }

    public async Task<Result<object>> UpdateNotificationSettingAsync(UpdateNotificationSettingRequest r)
    {
        var url = $"https://mybusinessnotifications.googleapis.com/v1/accounts/{r.AccountId}/notificationSetting";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "updateMask", "pubsubTopic,notificationTypes");
        return await SendAsync(r.AccountKey, "UpdateNotificationSetting", HttpMethod.Patch, url, query, new { name = $"accounts/{r.AccountId}/notificationSetting", pubsubTopic = r.PubsubTopic, notificationTypes = r.NotificationTypes });
    }
}
