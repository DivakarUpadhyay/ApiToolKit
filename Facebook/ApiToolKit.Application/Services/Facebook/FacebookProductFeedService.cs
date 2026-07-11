using ApiToolKit.Application.DTOs.Facebook.ProductFeed;
using ApiToolKit.Application.Interfaces.Services.Facebook;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Entities.Facebook;
using ApiToolKit.Domain.Interfaces.Repositories.Facebook;
using Newtonsoft.Json;
namespace ApiToolKit.Application.Services.Facebook;
public class FacebookProductFeedService : IFacebookProductFeedService
{
    private readonly IFacebookApiClient _apiClient;
    private readonly IFacebookProductFeedRepository _repo;
    public FacebookProductFeedService(IFacebookApiClient apiClient, IFacebookProductFeedRepository repo)
    {
        _apiClient = apiClient;
        _repo = repo;
    }
    private async Task LogAsync(string apiName, bool success, int statusCode = 200, string? request = null, string? response = null)
    {
        await _repo.SaveApiLogAsync(new FacebookApiLog
        {
            ApiName = apiName,
            RequestBody = request ?? string.Empty,
            ResponseBody = response ?? string.Empty,
            IsSuccess = success,
            StatusCode = statusCode,
            CreatedAt = DateTime.UtcNow
        });
    }

    public async Task<Result<ProductFeed>> CreateProductFeedAsync(string catalogId, CreateProductFeedRequest request, string accessToken)
    {
        try
        {
            var body = new
            {
                name = request.Name,
                schedule = request.ScheduleUrl != null ? new { url = request.ScheduleUrl, interval = request.Interval, hour = 0 } : null,
                format = request.Format,
                encoding = request.Encoding,
                country = request.Country,
                language = request.Language,
                default_currency = request.DefaultCurrency
            };
            var response = await _apiClient.PostAsync<dynamic>($"/{catalogId}/product_feeds", body, accessToken);
            if (response == null) return Result<ProductFeed>.Fail("No response from API.");
            var result = JsonConvert.DeserializeObject<ProductFeed>(JsonConvert.SerializeObject(response));
            await LogAsync("CreateProductFeed", true, 200, JsonConvert.SerializeObject(request), JsonConvert.SerializeObject(response));
            return result != null ? Result<ProductFeed>.Ok(result) : Result<ProductFeed>.Fail("Failed to parse response.");
        }
        catch (Exception ex)
        {
            await LogAsync("CreateProductFeed", false, 500, JsonConvert.SerializeObject(request), ex.Message);
            return Result<ProductFeed>.Fail(ex.Message);
        }
    }

    public async Task<Result<ProductFeedsResponse>> GetProductFeedsAsync(string catalogId, string accessToken)
    {
        try
        {
            var fields = "id,name,file_name,format,country,language,default_currency,encoding,schedule";
            var response = await _apiClient.GetAsync<dynamic>($"/{catalogId}/product_feeds?fields={fields}", accessToken: accessToken);
            if (response == null) return Result<ProductFeedsResponse>.Fail("No response from API.");
            var result = JsonConvert.DeserializeObject<ProductFeedsResponse>(JsonConvert.SerializeObject(response));
            await LogAsync("GetProductFeeds", true, 200, catalogId, JsonConvert.SerializeObject(response));
            return result != null ? Result<ProductFeedsResponse>.Ok(result) : Result<ProductFeedsResponse>.Fail("Failed to parse response.");
        }
        catch (Exception ex)
        {
            await LogAsync("GetProductFeeds", false, 500, catalogId, ex.Message);
            return Result<ProductFeedsResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<ProductFeed>> GetProductFeedAsync(string feedId, string accessToken)
    {
        try
        {
            var fields = "id,name,file_name,format,country,language,default_currency,encoding,schedule";
            var response = await _apiClient.GetAsync<dynamic>($"/{feedId}?fields={fields}", accessToken: accessToken);
            if (response == null) return Result<ProductFeed>.Fail("No response from API.");
            var result = JsonConvert.DeserializeObject<ProductFeed>(JsonConvert.SerializeObject(response));
            await LogAsync("GetProductFeed", true, 200, feedId, JsonConvert.SerializeObject(response));
            return result != null ? Result<ProductFeed>.Ok(result) : Result<ProductFeed>.Fail("Failed to parse response.");
        }
        catch (Exception ex)
        {
            await LogAsync("GetProductFeed", false, 500, feedId, ex.Message);
            return Result<ProductFeed>.Fail(ex.Message);
        }
    }

    public async Task<Result<bool>> DeleteProductFeedAsync(string feedId, string accessToken)
    {
        try
        {
            var response = await _apiClient.DeleteAsync<dynamic>($"/{feedId}", accessToken);
            await LogAsync("DeleteProductFeed", response != null, response != null ? 200 : 400, feedId, JsonConvert.SerializeObject(response));
            return response != null ? Result<bool>.Ok(true) : Result<bool>.Fail("Failed to delete product feed.");
        }
        catch (Exception ex)
        {
            await LogAsync("DeleteProductFeed", false, 500, feedId, ex.Message);
            return Result<bool>.Fail(ex.Message);
        }
    }

    public async Task<Result<FeedUpload>> UploadFeedFileAsync(string feedId, string accessToken, Stream fileStream, string fileName)
    {
        try
        {
            var multipart = new MultipartFormDataContent();
            multipart.Add(new StreamContent(fileStream), "file", fileName);
            var response = await _apiClient.PostMultipartAsync<dynamic>($"/{feedId}/uploads", multipart, accessToken);
            if (response == null) return Result<FeedUpload>.Fail("No response from API.");
            var result = JsonConvert.DeserializeObject<FeedUpload>(JsonConvert.SerializeObject(response));
            await LogAsync("UploadFeedFile", true, 200, feedId, JsonConvert.SerializeObject(response));
            return result != null ? Result<FeedUpload>.Ok(result) : Result<FeedUpload>.Fail("Failed to parse response.");
        }
        catch (Exception ex)
        {
            await LogAsync("UploadFeedFile", false, 500, feedId, ex.Message);
            return Result<FeedUpload>.Fail(ex.Message);
        }
    }

    public async Task<Result<FeedUploadsResponse>> GetFeedUploadsAsync(string feedId, string accessToken)
    {
        try
        {
            var fields = "id,start_time,end_time,url,num_persisted_items,num_detected_items,status,error_count,warning_count";
            var response = await _apiClient.GetAsync<dynamic>($"/{feedId}/uploads?fields={fields}", accessToken: accessToken);
            if (response == null) return Result<FeedUploadsResponse>.Fail("No response from API.");
            var result = JsonConvert.DeserializeObject<FeedUploadsResponse>(JsonConvert.SerializeObject(response));
            await LogAsync("GetFeedUploads", true, 200, feedId, JsonConvert.SerializeObject(response));
            return result != null ? Result<FeedUploadsResponse>.Ok(result) : Result<FeedUploadsResponse>.Fail("Failed to parse response.");
        }
        catch (Exception ex)
        {
            await LogAsync("GetFeedUploads", false, 500, feedId, ex.Message);
            return Result<FeedUploadsResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<FeedUpload>> GetFeedUploadAsync(string uploadId, string accessToken)
    {
        try
        {
            var fields = "id,start_time,end_time,url,num_persisted_items,num_detected_items,status,error_count,warning_count";
            var response = await _apiClient.GetAsync<dynamic>($"/{uploadId}?fields={fields}", accessToken: accessToken);
            if (response == null) return Result<FeedUpload>.Fail("No response from API.");
            var result = JsonConvert.DeserializeObject<FeedUpload>(JsonConvert.SerializeObject(response));
            await LogAsync("GetFeedUpload", true, 200, uploadId, JsonConvert.SerializeObject(response));
            return result != null ? Result<FeedUpload>.Ok(result) : Result<FeedUpload>.Fail("Failed to parse response.");
        }
        catch (Exception ex)
        {
            await LogAsync("GetFeedUpload", false, 500, uploadId, ex.Message);
            return Result<FeedUpload>.Fail(ex.Message);
        }
    }

    public async Task<Result<FeedErrorsResponse>> GetFeedErrorsAsync(string feedId, string accessToken)
    {
        try
        {
            var fields = "id,description,severity,summary,sample_affected_items_count";
            var response = await _apiClient.GetAsync<dynamic>($"/{feedId}/product_feed_item_error_set?fields={fields}", accessToken: accessToken);
            if (response == null) return Result<FeedErrorsResponse>.Fail("No response from API.");
            var result = JsonConvert.DeserializeObject<FeedErrorsResponse>(JsonConvert.SerializeObject(response));
            await LogAsync("GetFeedErrors", true, 200, feedId, JsonConvert.SerializeObject(response));
            return result != null ? Result<FeedErrorsResponse>.Ok(result) : Result<FeedErrorsResponse>.Fail("Failed to parse response.");
        }
        catch (Exception ex)
        {
            await LogAsync("GetFeedErrors", false, 500, feedId, ex.Message);
            return Result<FeedErrorsResponse>.Fail(ex.Message);
        }
    }
}
