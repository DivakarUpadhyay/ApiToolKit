using ApiToolKit.Application.DTOs.DisplayVideo.NegativeKeywordListNegativeKeyword;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.DisplayVideo;

public interface IDisplayVideoNegativeKeywordListNegativeKeywordService
{
    Task<Result<object>> BulkEditNegativeKeywordAsync(BulkEditNegativeKeywordRequest request);
    Task<Result<object>> DeleteNegativeKeywordAsync(DeleteNegativeKeywordRequest request);
    Task<Result<object>> ListNegativeKeywordsAsync(ListNegativeKeywordsRequest request);
    Task<Result<object>> ReplaceNegativeKeywordAsync(ReplaceNegativeKeywordRequest request);
    Task<Result<object>> CreateNegativeKeywordAsync(CreateNegativeKeywordRequest request);
}
