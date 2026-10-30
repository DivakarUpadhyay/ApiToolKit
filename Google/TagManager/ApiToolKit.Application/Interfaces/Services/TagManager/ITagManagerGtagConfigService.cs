using ApiToolKit.Application.DTOs.TagManager.GtagConfig;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.TagManager;

public interface ITagManagerGtagConfigService
{
    Task<Result<object>> ListGtagConfigsAsync(ListGtagConfigsRequest request);
    Task<Result<object>> GetGtagConfigAsync(GetGtagConfigRequest request);
    Task<Result<object>> CreateGtagConfigAsync(CreateGtagConfigRequest request);
    Task<Result<object>> UpdateGtagConfigAsync(UpdateGtagConfigRequest request);
    Task<Result<object>> DeleteGtagConfigAsync(DeleteGtagConfigRequest request);
}
