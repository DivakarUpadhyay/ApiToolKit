using ApiToolKit.Application.DTOs.Facebook.ProductFeed;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Facebook;

public interface IFacebookProductFeedService
{
    Task<Result<ProductFeed>> CreateProductFeedAsync(string catalogId, CreateProductFeedRequest request, string accessToken);
    Task<Result<ProductFeedsResponse>> GetProductFeedsAsync(string catalogId, string accessToken);
    Task<Result<ProductFeed>> GetProductFeedAsync(string feedId, string accessToken);
    Task<Result<bool>> DeleteProductFeedAsync(string feedId, string accessToken);
    Task<Result<FeedUpload>> UploadFeedFileAsync(string feedId, string accessToken, Stream fileStream, string fileName);
    Task<Result<FeedUploadsResponse>> GetFeedUploadsAsync(string feedId, string accessToken);
    Task<Result<FeedUpload>> GetFeedUploadAsync(string uploadId, string accessToken);
    Task<Result<FeedErrorsResponse>> GetFeedErrorsAsync(string feedId, string accessToken);
}
