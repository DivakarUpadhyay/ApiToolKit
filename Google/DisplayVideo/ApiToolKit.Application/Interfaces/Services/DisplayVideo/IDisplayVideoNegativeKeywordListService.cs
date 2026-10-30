using ApiToolKit.Application.DTOs.DisplayVideo.NegativeKeywordList;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.DisplayVideo;

public interface IDisplayVideoNegativeKeywordListService
{
    Task<Result<object>> DeleteNegativeKeywordListAsync(DeleteNegativeKeywordListRequest request);
    Task<Result<object>> ListNegativeKeywordListsAsync(ListNegativeKeywordListsRequest request);
    Task<Result<object>> UpdateNegativeKeywordListAsync(UpdateNegativeKeywordListRequest request);
    Task<Result<object>> GetNegativeKeywordListAsync(GetNegativeKeywordListRequest request);
    Task<Result<object>> CreateNegativeKeywordListAsync(CreateNegativeKeywordListRequest request);
}
