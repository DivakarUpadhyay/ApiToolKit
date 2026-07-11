using ApiToolKit.Application.DTOs.Facebook.LeadGen;
using ApiToolKit.Application.Interfaces.Services.Facebook;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.Facebook;
using Newtonsoft.Json;

namespace ApiToolKit.Application.Services.Facebook;

public class FacebookLeadGenService : IFacebookLeadGenService
{
    private readonly IFacebookAuthRepository _authRepo;
    private readonly IFacebookApiClient _apiClient;

    public FacebookLeadGenService(IFacebookAuthRepository authRepo, IFacebookApiClient apiClient)
    {
        _authRepo = authRepo;
        _apiClient = apiClient;
    }

    public async Task<Result<List<LeadGenFormResponse>>> GetLeadGenFormsAsync(GetLeadGenFormsRequest request, string accessToken)
    {
        try
        {
            var queryParams = new Dictionary<string, string>
            {
                ["fields"] = "id,name,status,created_time,leads_count,questions,privacy_policy,thank_you_page",
                ["limit"] = request.Limit.ToString(),
                ["access_token"] = accessToken
            };
            if (request.After != null) queryParams["after"] = request.After;

            var response = await _apiClient.GetAsync<dynamic>($"/{request.PageId}/leadgen_forms", queryParams);
            if (response == null) return Result<List<LeadGenFormResponse>>.Fail("No response from Facebook API.");

            var forms = JsonConvert.DeserializeObject<List<LeadGenFormResponse>>(
                JsonConvert.SerializeObject((object?)response?.data)) ?? new List<LeadGenFormResponse>();
            return Result<List<LeadGenFormResponse>>.Ok(forms);
        }
        catch (Exception ex) { return Result<List<LeadGenFormResponse>>.Fail(ex.Message); }
    }

    public async Task<Result<LeadGenFormResponse>> GetLeadGenFormAsync(string formId, string accessToken)
    {
        try
        {
            var queryParams = new Dictionary<string, string>
            {
                ["fields"] = "id,name,status,created_time,leads_count,questions,privacy_policy,thank_you_page,is_continued_flow",
                ["access_token"] = accessToken
            };

            var response = await _apiClient.GetAsync<dynamic>($"/{formId}", queryParams);
            if (response == null) return Result<LeadGenFormResponse>.Fail("No response from Facebook API.");

            var form = JsonConvert.DeserializeObject<LeadGenFormResponse>(JsonConvert.SerializeObject(response)) ?? new LeadGenFormResponse();
            return Result<LeadGenFormResponse>.Ok(form);
        }
        catch (Exception ex) { return Result<LeadGenFormResponse>.Fail(ex.Message); }
    }

    public async Task<Result<object>> CreateLeadGenFormAsync(CreateLeadGenFormRequest request, string accessToken)
    {
        try
        {
            var body = new Dictionary<string, object?>
            {
                ["name"] = request.Name,
                ["questions"] = request.Questions
            };
            if (request.PrivacyPolicy != null) body["privacy_policy"] = request.PrivacyPolicy;
            if (request.ThankYouPage != null) body["thank_you_page"] = request.ThankYouPage;
            if (request.Locale != null) body["locale"] = request.Locale;

            var response = await _apiClient.PostAsync<object>($"/{request.PageId}/leadgen_forms", body, accessToken);
            if (response == null) return Result<object>.Fail("No response from Facebook API.");
            return Result<object>.Ok(response);
        }
        catch (Exception ex) { return Result<object>.Fail(ex.Message); }
    }

    public async Task<Result<bool>> DeleteLeadGenFormAsync(string formId, string accessToken)
    {
        try
        {
            await _apiClient.DeleteAsync<object>($"/{formId}", accessToken);
            return Result<bool>.Ok(true);
        }
        catch (Exception ex) { return Result<bool>.Fail(ex.Message); }
    }

    public async Task<Result<List<LeadResponse>>> GetLeadsAsync(GetLeadsRequest request, string accessToken)
    {
        try
        {
            var queryParams = new Dictionary<string, string>
            {
                ["fields"] = "id,created_time,field_data,ad_id,ad_name,campaign_id,campaign_name,form_id",
                ["limit"] = request.Limit.ToString(),
                ["access_token"] = accessToken
            };
            if (request.After != null) queryParams["after"] = request.After;
            if (request.FilterStartTime != null) queryParams["filter_start_time"] = request.FilterStartTime;
            if (request.FilterEndTime != null) queryParams["filter_end_time"] = request.FilterEndTime;

            var response = await _apiClient.GetAsync<dynamic>($"/{request.FormId}/leads", queryParams);
            if (response == null) return Result<List<LeadResponse>>.Fail("No response from Facebook API.");

            var leads = JsonConvert.DeserializeObject<List<LeadResponse>>(
                JsonConvert.SerializeObject((object?)response?.data)) ?? new List<LeadResponse>();
            return Result<List<LeadResponse>>.Ok(leads);
        }
        catch (Exception ex) { return Result<List<LeadResponse>>.Fail(ex.Message); }
    }

    public async Task<Result<LeadResponse>> GetLeadAsync(string leadId, string accessToken)
    {
        try
        {
            var queryParams = new Dictionary<string, string>
            {
                ["fields"] = "id,created_time,field_data,ad_id,ad_name,campaign_id,campaign_name,form_id",
                ["access_token"] = accessToken
            };

            var response = await _apiClient.GetAsync<dynamic>($"/{leadId}", queryParams);
            if (response == null) return Result<LeadResponse>.Fail("No response from Facebook API.");

            var lead = JsonConvert.DeserializeObject<LeadResponse>(JsonConvert.SerializeObject(response)) ?? new LeadResponse();
            return Result<LeadResponse>.Ok(lead);
        }
        catch (Exception ex) { return Result<LeadResponse>.Fail(ex.Message); }
    }

    public async Task<Result<bool>> DeleteLeadAsync(string leadId, string accessToken)
    {
        try
        {
            await _apiClient.DeleteAsync<object>($"/{leadId}", accessToken);
            return Result<bool>.Ok(true);
        }
        catch (Exception ex) { return Result<bool>.Fail(ex.Message); }
    }
}
