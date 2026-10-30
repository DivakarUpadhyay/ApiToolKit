using ApiToolKit.Application.DTOs.BusinessProfile.Media;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.BusinessProfile;

public interface IBusinessProfileMediaService
{
    Task<Result<object>> ListMediaAsync(ListMediaRequest request);
    Task<Result<object>> GetMediaAsync(GetMediaRequest request);
    Task<Result<object>> CreateMediaAsync(CreateMediaRequest request);
    Task<Result<object>> UpdateMediaAsync(UpdateMediaRequest request);
    Task<Result<object>> DeleteMediaAsync(DeleteMediaRequest request);
    Task<Result<object>> StartMediaUploadAsync(StartMediaUploadRequest request);
    Task<Result<object>> ListCustomerMediaAsync(ListCustomerMediaRequest request);
    Task<Result<object>> GetCustomerMediaAsync(GetCustomerMediaRequest request);
}
