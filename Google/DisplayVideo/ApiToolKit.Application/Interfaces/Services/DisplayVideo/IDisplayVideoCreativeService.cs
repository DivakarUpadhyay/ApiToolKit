using ApiToolKit.Application.DTOs.DisplayVideo.Creative;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.DisplayVideo;

public interface IDisplayVideoCreativeService
{
    Task<Result<object>> UpdateCreativeAsync(UpdateCreativeRequest request);
    Task<Result<object>> CreateCreativeAsync(CreateCreativeRequest request);
    Task<Result<object>> GetCreativeAsync(GetCreativeRequest request);
    Task<Result<object>> ListCreativesAsync(ListCreativesRequest request);
    Task<Result<object>> DeleteCreativeAsync(DeleteCreativeRequest request);
}
