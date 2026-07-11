using ApiToolKit.Application.DTOs.Flipkart.Returns;
using ApiToolKit.Application.Interfaces.Services.Flipkart;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Services.Flipkart;

public class FlipkartReturnsService : IFlipkartReturnsService
{
    private readonly IFlipkartApiClient _api;
    public FlipkartReturnsService(IFlipkartApiClient api) => _api = api;

    public async Task<Result<ReturnsResponse>> GetReturnsAsync(GetReturnsRequest request)
    {
        var qs = BuildReturnQuery(request);
        var result = await _api.GetAsync<ReturnsResponse>("/v2/returns", qs);
        return result != null ? Result<ReturnsResponse>.Ok(result) : Result<ReturnsResponse>.Fail("No response.");
    }

    private static string BuildReturnQuery(GetReturnsRequest r)
    {
        var parts = new List<string>();
        if (r.Source != null) parts.Add($"source={r.Source}");
        if (r.ModifiedAfter != null) parts.Add($"modifiedAfter={r.ModifiedAfter}");
        if (r.ModifiedBefore != null) parts.Add($"modifiedBefore={r.ModifiedBefore}");
        if (r.CreatedAfter != null) parts.Add($"createdAfter={r.CreatedAfter}");
        if (r.CreatedBefore != null) parts.Add($"createdBefore={r.CreatedBefore}");
        if (r.LocationId != null) parts.Add($"locationId={r.LocationId}");
        if (r.ReturnIds != null) parts.Add($"returnIds={r.ReturnIds}");
        if (r.TrackingIds != null) parts.Add($"trackingIds={r.TrackingIds}");
        return string.Join("&", parts);
    }

    public async Task<Result<ReturnsResponse>> ApproveReturnsAsync(ApproveReturnsRequest request)
    {
        var result = await _api.PostAsync<ReturnsResponse>("/v2/returns/approve", request);
        return result != null ? Result<ReturnsResponse>.Ok(result) : Result<ReturnsResponse>.Fail("No response.");
    }

    public async Task<Result<ReturnsResponse>> RejectReturnsAsync(RejectReturnsRequest request)
    {
        var result = await _api.PostAsync<ReturnsResponse>("/v2/returns/reject", request);
        return result != null ? Result<ReturnsResponse>.Ok(result) : Result<ReturnsResponse>.Fail("No response.");
    }

    public async Task<Result<ReturnsResponse>> CompleteReturnsAsync(CompleteReturnsRequest request)
    {
        var qs = new List<string>();
        if (request.LocationId != null) qs.Add($"locationId={request.LocationId}");
        if (request.ReturnIds != null) qs.Add($"returnIds={request.ReturnIds}");
        var result = await _api.PostAsync<ReturnsResponse>("/v2/returns/complete", new { });
        return result != null ? Result<ReturnsResponse>.Ok(result) : Result<ReturnsResponse>.Fail("No response.");
    }

    public async Task<Result<ReturnsResponse>> PickupReturnsAsync(PickupReturnsRequest request)
    {
        var result = await _api.PostAsync<ReturnsResponse>("/v2/returns/pickup", request);
        return result != null ? Result<ReturnsResponse>.Ok(result) : Result<ReturnsResponse>.Fail("No response.");
    }

    public async Task<Result<ReturnsResponse>> PickupAttemptAsync(PickupAttemptRequest request)
    {
        var result = await _api.PostAsync<ReturnsResponse>("/v2/returns/pickupAttempt", request);
        return result != null ? Result<ReturnsResponse>.Ok(result) : Result<ReturnsResponse>.Fail("No response.");
    }

    public async Task<Result<ReturnsResponse>> InitialReturnLoadAsync(InitialReturnLoadRequest request)
    {
        var result = await _api.PostAsync<ReturnsResponse>("/v3/returns/tasks/initial-load", request);
        return result != null ? Result<ReturnsResponse>.Ok(result) : Result<ReturnsResponse>.Fail("No response.");
    }

    public async Task<Result<ReturnsResponse>> StartOtcReturnAsync(StartOtcReturnRequest request)
    {
        var result = await _api.PostAsync<ReturnsResponse>("/v3/returns/otc/start/details", request);
        return result != null ? Result<ReturnsResponse>.Ok(result) : Result<ReturnsResponse>.Fail("No response.");
    }

    public async Task<Result<ReturnsResponse>> GetReturnTaskShipmentsAsync(ReturnTaskShipmentsRequest request)
    {
        var result = await _api.PostAsync<ReturnsResponse>("/v3/returns/tasks/shipments", request);
        return result != null ? Result<ReturnsResponse>.Ok(result) : Result<ReturnsResponse>.Fail("No response.");
    }

    public async Task<Result<ReturnsResponse>> EndOtcReturnAsync(EndOtcReturnRequest request)
    {
        var result = await _api.PostAsync<ReturnsResponse>("/v3/returns/otc/end/details", request);
        return result != null ? Result<ReturnsResponse>.Ok(result) : Result<ReturnsResponse>.Fail("No response.");
    }
}
