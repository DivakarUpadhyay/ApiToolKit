using ApiToolKit.Application.DTOs.Flipkart.Returns;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Flipkart;

public interface IFlipkartReturnsService
{
    Task<Result<ReturnsResponse>> GetReturnsAsync(GetReturnsRequest request);
    Task<Result<ReturnsResponse>> ApproveReturnsAsync(ApproveReturnsRequest request);
    Task<Result<ReturnsResponse>> RejectReturnsAsync(RejectReturnsRequest request);
    Task<Result<ReturnsResponse>> CompleteReturnsAsync(CompleteReturnsRequest request);
    Task<Result<ReturnsResponse>> PickupReturnsAsync(PickupReturnsRequest request);
    Task<Result<ReturnsResponse>> PickupAttemptAsync(PickupAttemptRequest request);
    Task<Result<ReturnsResponse>> InitialReturnLoadAsync(InitialReturnLoadRequest request);
    Task<Result<ReturnsResponse>> StartOtcReturnAsync(StartOtcReturnRequest request);
    Task<Result<ReturnsResponse>> GetReturnTaskShipmentsAsync(ReturnTaskShipmentsRequest request);
    Task<Result<ReturnsResponse>> EndOtcReturnAsync(EndOtcReturnRequest request);
}
