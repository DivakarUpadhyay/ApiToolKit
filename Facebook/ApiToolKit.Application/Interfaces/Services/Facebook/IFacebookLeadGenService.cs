using ApiToolKit.Application.DTOs.Facebook.LeadGen;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Facebook;

public interface IFacebookLeadGenService
{
    Task<Result<List<LeadGenFormResponse>>> GetLeadGenFormsAsync(GetLeadGenFormsRequest request, string accessToken);
    Task<Result<LeadGenFormResponse>> GetLeadGenFormAsync(string formId, string accessToken);
    Task<Result<object>> CreateLeadGenFormAsync(CreateLeadGenFormRequest request, string accessToken);
    Task<Result<bool>> DeleteLeadGenFormAsync(string formId, string accessToken);
    Task<Result<List<LeadResponse>>> GetLeadsAsync(GetLeadsRequest request, string accessToken);
    Task<Result<LeadResponse>> GetLeadAsync(string leadId, string accessToken);
    Task<Result<bool>> DeleteLeadAsync(string leadId, string accessToken);
}
