using ApiToolKit.Application.DTOs.DisplayVideo.AdAsset;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.DisplayVideo;

public interface IDisplayVideoAdAssetService
{
    Task<Result<object>> ListAdAssetsAsync(ListAdAssetsRequest request);
    Task<Result<object>> BulkCreateAdAssetAsync(BulkCreateAdAssetRequest request);
    Task<Result<object>> CreateAdAssetAsync(CreateAdAssetRequest request);
    Task<Result<object>> GetAdAssetAsync(GetAdAssetRequest request);
    Task<Result<object>> UpdateAdAssetAsync(UpdateAdAssetRequest request);
}
