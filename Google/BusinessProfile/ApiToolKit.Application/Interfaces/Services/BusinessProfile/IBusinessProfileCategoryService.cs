using ApiToolKit.Application.DTOs.BusinessProfile.Category;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.BusinessProfile;

public interface IBusinessProfileCategoryService
{
    Task<Result<object>> ListCategoriesAsync(ListCategoriesRequest request);
    Task<Result<object>> BatchGetCategoriesAsync(BatchGetCategoriesRequest request);
    Task<Result<object>> ListAttributeMetadataAsync(ListAttributeMetadataRequest request);
    Task<Result<object>> GetChainAsync(GetChainRequest request);
    Task<Result<object>> SearchChainsAsync(SearchChainsRequest request);
}
