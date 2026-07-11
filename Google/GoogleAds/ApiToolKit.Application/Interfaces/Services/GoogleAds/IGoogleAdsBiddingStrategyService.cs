using ApiToolKit.Application.DTOs.GoogleAds.BiddingStrategy;
using ApiToolKit.Application.DTOs.GoogleAds.Campaign;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.GoogleAds;

public interface IGoogleAdsBiddingStrategyService
{
    Task<Result<List<BiddingStrategyResponse>>> ListBiddingStrategiesAsync(ListBiddingStrategiesRequest request);
    Task<Result<MutateOperationResponse>> CreateBiddingStrategyAsync(CreateBiddingStrategyRequest request);
}
