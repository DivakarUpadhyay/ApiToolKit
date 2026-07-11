using ApiToolKit.Application.DTOs.GoogleAds.Budget;
using ApiToolKit.Application.DTOs.GoogleAds.Campaign;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.GoogleAds;

public interface IGoogleAdsBudgetService
{
    Task<Result<List<BudgetResponse>>> ListBudgetsAsync(ListBudgetsRequest request);
    Task<Result<MutateOperationResponse>> CreateBudgetAsync(CreateBudgetRequest request);
}
