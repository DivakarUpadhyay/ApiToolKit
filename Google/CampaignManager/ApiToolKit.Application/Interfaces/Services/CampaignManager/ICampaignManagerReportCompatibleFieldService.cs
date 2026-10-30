using ApiToolKit.Application.DTOs.CampaignManager.ReportCompatibleField;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.CampaignManager;

public interface ICampaignManagerReportCompatibleFieldService
{
    Task<Result<object>> QueryCompatibleFieldAsync(QueryCompatibleFieldRequest request);
}
