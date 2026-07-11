using ApiToolKit.Application.DTOs.Facebook.WhatsAppFlows;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Facebook;

public interface IFacebookWhatsAppFlowsService
{
    Task<Result<List<WhatsAppFlowResponse>>> GetFlowsAsync(GetWhatsAppFlowsRequest request, string accessToken);
    Task<Result<WhatsAppFlowResponse>> GetFlowAsync(string flowId, string accessToken);
    Task<Result<object>> CreateFlowAsync(CreateWhatsAppFlowRequest request, string accessToken);
    Task<Result<bool>> UpdateFlowAsync(UpdateWhatsAppFlowRequest request, string accessToken);
    Task<Result<bool>> DeleteFlowAsync(string flowId, string accessToken);
    Task<Result<bool>> PublishFlowAsync(string flowId, string accessToken);
    Task<Result<bool>> DeprecateFlowAsync(string flowId, string accessToken);
    Task<Result<List<WhatsAppFlowAsset>>> GetFlowAssetsAsync(string flowId, string accessToken);
    Task<Result<object>> UploadFlowJsonAsync(UploadFlowJsonRequest request, string accessToken);
    Task<Result<WhatsAppFlowPreview>> GetFlowPreviewAsync(string flowId, string accessToken);
}
