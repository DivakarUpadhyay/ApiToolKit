using ApiToolKit.Application.DTOs.CampaignManager.BillingAssignment;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.CampaignManager;

namespace ApiToolKit.Application.Services.CampaignManager;

public class CampaignManagerBillingAssignmentService : CampaignManagerServiceBase, ICampaignManagerBillingAssignmentService
{
    public CampaignManagerBillingAssignmentService(ICampaignManagerApiClient apiClient, ICampaignManagerAuthService authService, ICampaignManagerAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> InsertBillingAssignmentAsync(InsertBillingAssignmentRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/billingProfiles/{Esc(r.BillingProfileId)}/billingAssignments";
        return await SendAsync(r.AccountKey, "InsertBillingAssignment", HttpMethod.Post, url, null, r.Body);
    }

    public async Task<Result<object>> ListBillingAssignmentsAsync(ListBillingAssignmentsRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/billingProfiles/{Esc(r.BillingProfileId)}/billingAssignments";
        return await SendAsync(r.AccountKey, "ListBillingAssignments", HttpMethod.Get, url);
    }
}
