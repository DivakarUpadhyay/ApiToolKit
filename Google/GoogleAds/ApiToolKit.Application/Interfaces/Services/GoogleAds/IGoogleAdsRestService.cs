using ApiToolKit.Domain.Common;
using System.Text.Json.Nodes;

namespace ApiToolKit.Application.Interfaces.Services.GoogleAds;

/// <summary>
/// Calls any Google Ads REST method with the stored OAuth token of a customer.
/// </summary>
public interface IGoogleAdsRestService
{
    /// <param name="customerId">Customer whose stored OAuth token is used.</param>
    /// <param name="apiName">Name written to the API log.</param>
    /// <param name="method">HTTP method.</param>
    /// <param name="path">Path after the version, e.g. customers/123/campaigns:mutate.</param>
    /// <param name="query">Query string values (null or empty values are skipped).</param>
    /// <param name="body">JSON body exactly as documented by Google.</param>
    Task<Result<JsonNode>> SendAsync(string customerId, string apiName, HttpMethod method, string path,
        IEnumerable<KeyValuePair<string, string?>>? query = null, JsonNode? body = null);
}
