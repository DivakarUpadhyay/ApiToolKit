using ApiToolKit.Application.DTOs.CampaignManager.DynamicProfile;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.CampaignManager;

public interface ICampaignManagerDynamicProfileService
{
    Task<Result<object>> GenerateCodeDynamicProfileAsync(GenerateCodeDynamicProfileRequest request);
    Task<Result<object>> UpdateDynamicProfileAsync(UpdateDynamicProfileRequest request);
    Task<Result<object>> InsertDynamicProfileAsync(InsertDynamicProfileRequest request);
    Task<Result<object>> PublishDynamicProfileAsync(PublishDynamicProfileRequest request);
    Task<Result<object>> GetDynamicProfileAsync(GetDynamicProfileRequest request);
}
