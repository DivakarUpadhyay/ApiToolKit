using ApiToolKit.Application.DTOs.Shopee.Return;
using ApiToolKit.Application.Interfaces.Services.Shopee;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.Shopee;
namespace ApiToolKit.Application.Services.Shopee;
public class ShopeeReturnService : IShopeeReturnService
{
    private readonly IShopeeAuthRepository _authRepo;
    private readonly IShopeeApiClient _apiClient;
    public ShopeeReturnService(IShopeeAuthRepository authRepo, IShopeeApiClient apiClient) { _authRepo = authRepo; _apiClient = apiClient; }
    private async Task<string?> GetToken(long shopId) { var t = await _authRepo.GetTokenByShopIdAsync(shopId); return t?.AccessToken; }
    public async Task<Result<ReturnListResponse>> GetReturnListAsync(GetReturnListRequest request) {
        var token = await GetToken(request.ShopId); if (token == null) return Result<ReturnListResponse>.Fail("Access token not found.");
        var q = new Dictionary<string, string> { ["page_no"] = request.PageNo.ToString(), ["page_size"] = request.PageSize.ToString() };
        if (request.CreateTimeFrom > 0) q["create_time_from"] = request.CreateTimeFrom.ToString();
        if (request.CreateTimeTo > 0) q["create_time_to"] = request.CreateTimeTo.ToString();
        if (!string.IsNullOrEmpty(request.Status)) q["status"] = request.Status;
        var r = await _apiClient.GetAsync<ReturnListResponse>("/api/v2/returns/get_return_list", q, token, request.ShopId);
        return r == null ? Result<ReturnListResponse>.Fail("No response.") : Result<ReturnListResponse>.Ok(r);
    }
    public async Task<Result<ReturnResponse>> GetReturnDetailAsync(GetReturnDetailRequest request) {
        var token = await GetToken(request.ShopId); if (token == null) return Result<ReturnResponse>.Fail("Access token not found.");
        var q = new Dictionary<string, string> { ["return_sn"] = request.ReturnSn };
        var r = await _apiClient.GetAsync<ReturnResponse>("/api/v2/returns/get_return_detail", q, token, request.ShopId);
        return r == null ? Result<ReturnResponse>.Fail("No response.") : Result<ReturnResponse>.Ok(r);
    }
    public async Task<Result<bool>> ConfirmReturnAsync(ConfirmReturnRequest request) {
        var token = await GetToken(request.ShopId); if (token == null) return Result<bool>.Fail("Access token not found.");
        await _apiClient.PostAsync<dynamic>("/api/v2/returns/confirm", new { return_sn = request.ReturnSn }, token, request.ShopId);
        return Result<bool>.Ok(true);
    }
    public async Task<Result<bool>> DisputeReturnAsync(DisputeReturnRequest request) {
        var token = await GetToken(request.ShopId); if (token == null) return Result<bool>.Fail("Access token not found.");
        var body = new { return_sn = request.ReturnSn, email = "", dispute_reason = request.DisputeReason, dispute_text_reason = request.DisputeTextReason, image_list = request.ImageList };
        await _apiClient.PostAsync<dynamic>("/api/v2/returns/dispute", body, token, request.ShopId);
        return Result<bool>.Ok(true);
    }
    public async Task<Result<bool>> OfferReturnAsync(OfferReturnRequest request) {
        var token = await GetToken(request.ShopId); if (token == null) return Result<bool>.Fail("Access token not found.");
        await _apiClient.PostAsync<dynamic>("/api/v2/returns/offer", new { return_sn = request.ReturnSn, offer_type = request.OfferType, amount = request.Amount }, token, request.ShopId);
        return Result<bool>.Ok(true);
    }
    public async Task<Result<bool>> AcceptOfferAsync(AcceptOfferRequest request) {
        var token = await GetToken(request.ShopId); if (token == null) return Result<bool>.Fail("Access token not found.");
        await _apiClient.PostAsync<dynamic>("/api/v2/returns/accept_offer", new { return_sn = request.ReturnSn }, token, request.ShopId);
        return Result<bool>.Ok(true);
    }
    public async Task<Result<bool>> CancelDisputeAsync(CancelDisputeRequest request) {
        var token = await GetToken(request.ShopId); if (token == null) return Result<bool>.Fail("Access token not found.");
        await _apiClient.PostAsync<dynamic>("/api/v2/returns/cancel_dispute", new { return_sn = request.ReturnSn }, token, request.ShopId);
        return Result<bool>.Ok(true);
    }
    public async Task<Result<bool>> UploadProofAsync(UploadProofRequest request) {
        var token = await GetToken(request.ShopId); if (token == null) return Result<bool>.Fail("Access token not found.");
        await _apiClient.PostAsync<dynamic>("/api/v2/returns/upload_proof", new { return_sn = request.ReturnSn, text = request.Text, image_list = request.ImageList, video_list = request.VideoList }, token, request.ShopId);
        return Result<bool>.Ok(true);
    }
    public async Task<Result<object>> GetReturnDisputeReasonAsync(GetReturnDisputeReasonRequest request) {
        var token = await GetToken(request.ShopId); if (token == null) return Result<object>.Fail("Access token not found.");
        var r = await _apiClient.GetAsync<object>("/api/v2/returns/get_return_dispute_reason", new Dictionary<string, string>(), token, request.ShopId);
        return r == null ? Result<object>.Fail("No response.") : Result<object>.Ok(r);
    }
    public async Task<Result<object>> GetAvailableSolutionsAsync(long shopId, string returnSn) {
        var token = await GetToken(shopId); if (token == null) return Result<object>.Fail("Access token not found.");
        var r = await _apiClient.GetAsync<object>("/api/v2/returns/get_available_solutions", new Dictionary<string, string> { ["return_sn"] = returnSn }, token, shopId);
        return r == null ? Result<object>.Fail("No response.") : Result<object>.Ok(r);
    }
    public async Task<Result<object>> GetShippingCarrierAsync(long shopId, string returnSn) {
        var token = await GetToken(shopId); if (token == null) return Result<object>.Fail("Access token not found.");
        var r = await _apiClient.GetAsync<object>("/api/v2/returns/get_shipping_carrier", new Dictionary<string, string> { ["return_sn"] = returnSn }, token, shopId);
        return r == null ? Result<object>.Fail("No response.") : Result<object>.Ok(r);
    }
    public async Task<Result<object>> QueryProofAsync(long shopId, string returnSn) {
        var token = await GetToken(shopId); if (token == null) return Result<object>.Fail("Access token not found.");
        var r = await _apiClient.GetAsync<object>("/api/v2/returns/query_proof", new Dictionary<string, string> { ["return_sn"] = returnSn }, token, shopId);
        return r == null ? Result<object>.Fail("No response.") : Result<object>.Ok(r);
    }
    public async Task<Result<object>> GetReverseTrackingInfoAsync(long shopId, string returnSn) {
        var token = await GetToken(shopId); if (token == null) return Result<object>.Fail("Access token not found.");
        var r = await _apiClient.GetAsync<object>("/api/v2/returns/get_reverse_tracking_info", new Dictionary<string, string> { ["return_sn"] = returnSn }, token, shopId);
        return r == null ? Result<object>.Fail("No response.") : Result<object>.Ok(r);
    }
    public async Task<Result<bool>> UploadShippingProofAsync(long shopId, string returnSn, string trackingNumber, string carrierId) {
        var token = await GetToken(shopId); if (token == null) return Result<bool>.Fail("Access token not found.");
        await _apiClient.PostAsync<dynamic>("/api/v2/returns/upload_shipping_proof", new { return_sn = returnSn, tracking_number = trackingNumber, carrier_id = carrierId }, token, shopId);
        return Result<bool>.Ok(true);
    }
    public async Task<Result<object>> ConvertImageAsync(long shopId, List<string> imageUrlList) {
        var token = await GetToken(shopId); if (token == null) return Result<object>.Fail("Access token not found.");
        var r = await _apiClient.PostAsync<object>("/api/v2/returns/convert_image", new { image_url_list = imageUrlList }, token, shopId);
        return r == null ? Result<object>.Fail("No response.") : Result<object>.Ok(r);
    }
}
