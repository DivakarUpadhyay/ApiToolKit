using ApiToolKit.Application.DTOs.CampaignManager.TargetingTemplate;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.CampaignManager;

public interface ICampaignManagerTargetingTemplateService
{
    Task<Result<object>> InsertTargetingTemplateAsync(InsertTargetingTemplateRequest request);
    Task<Result<object>> UpdateTargetingTemplateAsync(UpdateTargetingTemplateRequest request);
    Task<Result<object>> GetTargetingTemplateAsync(GetTargetingTemplateRequest request);
    Task<Result<object>> UpdateTargetingTemplateAltAsync(UpdateTargetingTemplateAltRequest request);
    Task<Result<object>> ListTargetingTemplatesAsync(ListTargetingTemplatesRequest request);
}
