using ApiToolKit.Application.DTOs.Search.CustomSearch;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Search;

public interface ICustomSearchService
{
    Task<Result<CustomSearchResponse>> SearchAsync(CustomSearchRequest request);
    Task<Result<CustomSearchResponse>> ImageSearchAsync(ImageSearchRequest request);
}
