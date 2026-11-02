using ApiToolKit.Application.DTOs.Facebook.WhatsAppFlows;
using ApiToolKit.Application.Interfaces.Services.Facebook;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.Facebook;
using Newtonsoft.Json;

namespace ApiToolKit.Application.Services.Facebook;

public class FacebookWhatsAppFlowsService : IFacebookWhatsAppFlowsService
{
    private readonly IFacebookAuthRepository _authRepo;
    private readonly IWhatsAppApiClient _apiClient;

    public FacebookWhatsAppFlowsService(IFacebookAuthRepository authRepo, IWhatsAppApiClient apiClient)
    {
        _authRepo = authRepo;
        _apiClient = apiClient;
    }

    public async Task<Result<List<WhatsAppFlowResponse>>> GetFlowsAsync(GetWhatsAppFlowsRequest request, string accessToken)
    {
        try
        {
            var queryParams = new Dictionary<string, string>
            {
                ["fields"] = "id,name,status,categories,json_version,data_api_version,endpoint_uri,preview,whatsapp_business_account",
                ["limit"] = request.Limit.ToString(),
            };
            if (request.After != null) queryParams["after"] = request.After;

            var response = await _apiClient.GetAsync<dynamic>($"/{request.WabaId}/flows", accessToken, queryParams);
            if (response == null) return Result<List<WhatsAppFlowResponse>>.Fail("No response from Facebook API.");

            var flows = JsonConvert.DeserializeObject<List<WhatsAppFlowResponse>>(
                JsonConvert.SerializeObject((object?)response?.data)) ?? new List<WhatsAppFlowResponse>();
            return Result<List<WhatsAppFlowResponse>>.Ok(flows);
        }
        catch (Exception ex) { return Result<List<WhatsAppFlowResponse>>.Fail(ex.Message); }
    }

    public async Task<Result<WhatsAppFlowResponse>> GetFlowAsync(string flowId, string accessToken)
    {
        try
        {
            var queryParams = new Dictionary<string, string>
            {
                ["fields"] = "id,name,status,categories,json_version,data_api_version,endpoint_uri,preview,whatsapp_business_account,validation_errors",
            };

            var response = await _apiClient.GetAsync<dynamic>($"/{flowId}", accessToken, queryParams);
            if (response == null) return Result<WhatsAppFlowResponse>.Fail("No response from Facebook API.");

            var flow = JsonConvert.DeserializeObject<WhatsAppFlowResponse>(JsonConvert.SerializeObject(response)) ?? new WhatsAppFlowResponse();
            return Result<WhatsAppFlowResponse>.Ok(flow);
        }
        catch (Exception ex) { return Result<WhatsAppFlowResponse>.Fail(ex.Message); }
    }

    public async Task<Result<object>> CreateFlowAsync(CreateWhatsAppFlowRequest request, string accessToken)
    {
        try
        {
            var body = new Dictionary<string, object?>
            {
                ["name"] = request.Name,
                ["categories"] = request.Categories
            };
            if (request.EndpointUri != null) body["endpoint_uri"] = request.EndpointUri;
            if (request.FlowJson != null) body["flow_json"] = request.FlowJson;

            var response = await _apiClient.PostAsync<object>($"/{request.WabaId}/flows", accessToken, body);
            if (response == null) return Result<object>.Fail("No response from Facebook API.");
            return Result<object>.Ok(response);
        }
        catch (Exception ex) { return Result<object>.Fail(ex.Message); }
    }

    public async Task<Result<bool>> UpdateFlowAsync(UpdateWhatsAppFlowRequest request, string accessToken)
    {
        try
        {
            var body = new Dictionary<string, object?>();
            if (request.Name != null) body["name"] = request.Name;
            if (request.Categories != null) body["categories"] = request.Categories;
            if (request.EndpointUri != null) body["endpoint_uri"] = request.EndpointUri;
            if (request.ApplicationId != null) body["application_id"] = request.ApplicationId;

            await _apiClient.PostAsync<object>($"/{request.FlowId}", accessToken, body);
            return Result<bool>.Ok(true);
        }
        catch (Exception ex) { return Result<bool>.Fail(ex.Message); }
    }

    public async Task<Result<bool>> DeleteFlowAsync(string flowId, string accessToken)
    {
        try
        {
            await _apiClient.DeleteAsync<object>($"/{flowId}", accessToken);
            return Result<bool>.Ok(true);
        }
        catch (Exception ex) { return Result<bool>.Fail(ex.Message); }
    }

    public async Task<Result<bool>> PublishFlowAsync(string flowId, string accessToken)
    {
        try
        {
            await _apiClient.PostAsync<object>($"/{flowId}/publish", accessToken, null);
            return Result<bool>.Ok(true);
        }
        catch (Exception ex) { return Result<bool>.Fail(ex.Message); }
    }

    public async Task<Result<bool>> DeprecateFlowAsync(string flowId, string accessToken)
    {
        try
        {
            await _apiClient.PostAsync<object>($"/{flowId}/deprecate", accessToken, null);
            return Result<bool>.Ok(true);
        }
        catch (Exception ex) { return Result<bool>.Fail(ex.Message); }
    }

    public async Task<Result<List<WhatsAppFlowAsset>>> GetFlowAssetsAsync(string flowId, string accessToken)
    {
        try
        {
            var queryParams = new Dictionary<string, string>();

            var response = await _apiClient.GetAsync<dynamic>($"/{flowId}/assets", accessToken, queryParams);
            if (response == null) return Result<List<WhatsAppFlowAsset>>.Fail("No response from Facebook API.");

            var assets = JsonConvert.DeserializeObject<List<WhatsAppFlowAsset>>(
                JsonConvert.SerializeObject((object?)response?.data)) ?? new List<WhatsAppFlowAsset>();
            return Result<List<WhatsAppFlowAsset>>.Ok(assets);
        }
        catch (Exception ex) { return Result<List<WhatsAppFlowAsset>>.Fail(ex.Message); }
    }

    public async Task<Result<object>> UploadFlowJsonAsync(UploadFlowJsonRequest request, string accessToken)
    {
        try
        {
            var content = new MultipartFormDataContent();
            var jsonBytes = System.Text.Encoding.UTF8.GetBytes(request.FlowJson);
            var file = new ByteArrayContent(jsonBytes);
            file.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            content.Add(file, "file", "flow.json");
            // Meta requires name=flow.json and asset_type=FLOW_JSON alongside the file.
            content.Add(new StringContent("flow.json"), "name");
            content.Add(new StringContent("FLOW_JSON"), "asset_type");

            var response = await _apiClient.PostMultipartAsync<object>($"/{request.FlowId}/assets", accessToken, content);
            if (response == null) return Result<object>.Fail("No response from Facebook API.");
            return Result<object>.Ok(response);
        }
        catch (Exception ex) { return Result<object>.Fail(ex.Message); }
    }

    public async Task<Result<WhatsAppFlowPreview>> GetFlowPreviewAsync(string flowId, string accessToken)
    {
        try
        {
            var queryParams = new Dictionary<string, string>
            {
                ["fields"] = "preview",
            };

            var response = await _apiClient.GetAsync<dynamic>($"/{flowId}", accessToken, queryParams);
            if (response == null) return Result<WhatsAppFlowPreview>.Fail("No response from Facebook API.");

            var preview = JsonConvert.DeserializeObject<WhatsAppFlowPreview>(
                JsonConvert.SerializeObject((object?)response?.preview)) ?? new WhatsAppFlowPreview();
            return Result<WhatsAppFlowPreview>.Ok(preview);
        }
        catch (Exception ex) { return Result<WhatsAppFlowPreview>.Fail(ex.Message); }
    }
}
