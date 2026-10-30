using ApiToolKit.Application.DTOs.BusinessProfile.LocalPost;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.BusinessProfile;

public interface IBusinessProfileLocalPostService
{
    Task<Result<object>> ListLocalPostsAsync(ListLocalPostsRequest request);
    Task<Result<object>> CreateLocalPostAsync(CreateLocalPostRequest request);
    Task<Result<object>> DeleteLocalPostAsync(DeleteLocalPostRequest request);
    Task<Result<object>> GetLocalPostAsync(GetLocalPostRequest request);
    Task<Result<object>> UpdateLocalPostAsync(UpdateLocalPostRequest request);
    Task<Result<object>> GetLocalPostInsightsAsync(GetLocalPostInsightsRequest request);
}
