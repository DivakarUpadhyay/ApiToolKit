using ApiToolKit.Application.DTOs.Shopee.Return;
using ApiToolKit.Domain.Common;
namespace ApiToolKit.Application.Interfaces.Services.Shopee;
public interface IShopeeReturnService
{
    Task<Result<ReturnListResponse>> GetReturnListAsync(GetReturnListRequest request);
    Task<Result<ReturnResponse>> GetReturnDetailAsync(GetReturnDetailRequest request);
    Task<Result<bool>> ConfirmReturnAsync(ConfirmReturnRequest request);
    Task<Result<bool>> DisputeReturnAsync(DisputeReturnRequest request);
    Task<Result<bool>> OfferReturnAsync(OfferReturnRequest request);
    Task<Result<bool>> AcceptOfferAsync(AcceptOfferRequest request);
    Task<Result<bool>> CancelDisputeAsync(CancelDisputeRequest request);
    Task<Result<bool>> UploadProofAsync(UploadProofRequest request);
    Task<Result<object>> GetReturnDisputeReasonAsync(GetReturnDisputeReasonRequest request);
    Task<Result<object>> GetAvailableSolutionsAsync(long shopId, string returnSn);
    Task<Result<object>> GetShippingCarrierAsync(long shopId, string returnSn);
    Task<Result<object>> QueryProofAsync(long shopId, string returnSn);
    Task<Result<object>> GetReverseTrackingInfoAsync(long shopId, string returnSn);
    Task<Result<bool>> UploadShippingProofAsync(long shopId, string returnSn, string trackingNumber, string carrierId);
    Task<Result<object>> ConvertImageAsync(long shopId, List<string> imageUrlList);
}
