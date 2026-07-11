using ApiToolKit.Application.DTOs.Facebook.Capi;
using ApiToolKit.Domain.Common;
namespace ApiToolKit.Application.Interfaces.Services.Facebook;
public interface IFacebookCapiService {
    Task<Result<CApiEventsResponse>> SendEventsAsync(SendCApiEventsRequest request);
    Task<Result<CApiEventsResponse>> SendPurchaseEventAsync(PurchaseEventRequest request);
    Task<Result<CApiEventsResponse>> SendAddToCartEventAsync(AddToCartEventRequest request);
    Task<Result<CApiEventsResponse>> SendPageViewEventAsync(PageViewEventRequest request);
    Task<Result<CApiEventsResponse>> SendLeadEventAsync(LeadEventRequest request);
}
