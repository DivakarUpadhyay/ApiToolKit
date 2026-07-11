using ApiToolKit.Application.DTOs.YouTube.Search;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.YouTube;

public interface IYouTubeSearchService
{
    Task<Result<SearchResponse>> SearchAsync(SearchRequest request, string accessToken);
}
