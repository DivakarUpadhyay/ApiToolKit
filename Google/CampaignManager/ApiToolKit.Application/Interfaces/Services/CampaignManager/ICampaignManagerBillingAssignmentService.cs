using ApiToolKit.Application.DTOs.CampaignManager.BillingAssignment;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.CampaignManager;

public interface ICampaignManagerBillingAssignmentService
{
    Task<Result<object>> InsertBillingAssignmentAsync(InsertBillingAssignmentRequest request);
    Task<Result<object>> ListBillingAssignmentsAsync(ListBillingAssignmentsRequest request);
}
