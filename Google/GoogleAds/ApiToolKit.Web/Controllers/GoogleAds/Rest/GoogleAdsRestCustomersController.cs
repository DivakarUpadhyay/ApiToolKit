using ApiToolKit.Application.Interfaces.Services.GoogleAds;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Nodes;

namespace ApiToolKit.Web.Controllers.GoogleAds.Rest;

/// <summary>
/// Google Ads API v26 - Customers methods. Request and response bodies are passed through exactly as Google documents them.
/// </summary>
[ApiController]
[Route("api/googleads/rest")]
[Produces("application/json")]
public class GoogleAdsRestCustomersController : ControllerBase
{
    private readonly IGoogleAdsRestService _restService;

    public GoogleAdsRestCustomersController(IGoogleAdsRestService restService)
    {
        _restService = restService;
    }

    /// <summary>Returns insights for a collection of YouTube Creators and Channels.</summary>
    /// <remarks>Body: GoogleAdsGoogleadsV26Services__GenerateCreatorInsightsRequest JSON as documented at developers.google.com/google-ads/api/rest/reference/rest/v26.</remarks>
    [HttpPost("customers/{customerId}/generateCreatorInsights")]
    public async Task<IActionResult> GenerateCreatorInsightsAsync([FromRoute] string customerId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] JsonObject? body)
    {
        var result = await _restService.SendAsync(customerId, "Customers.GenerateCreatorInsights", HttpMethod.Post, $"customers/{Uri.EscapeDataString(customerId)}:generateCreatorInsights", null, body);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Uploads the given user data.</summary>
    /// <remarks>Body: GoogleAdsGoogleadsV26Services__UploadUserDataRequest JSON as documented at developers.google.com/google-ads/api/rest/reference/rest/v26.</remarks>
    [HttpPost("customers/{customerId}/uploadUserData")]
    public async Task<IActionResult> UploadUserDataAsync([FromRoute] string customerId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] JsonObject? body)
    {
        var result = await _restService.SendAsync(customerId, "Customers.UploadUserData", HttpMethod.Post, $"customers/{Uri.EscapeDataString(customerId)}:uploadUserData", null, body);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Updates a customer.</summary>
    /// <remarks>Body: GoogleAdsGoogleadsV26Services__MutateCustomerRequest JSON as documented at developers.google.com/google-ads/api/rest/reference/rest/v26.</remarks>
    [HttpPost("customers/{customerId}/mutate")]
    public async Task<IActionResult> MutateAsync([FromRoute] string customerId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] JsonObject? body)
    {
        var result = await _restService.SendAsync(customerId, "Customers.Mutate", HttpMethod.Post, $"customers/{Uri.EscapeDataString(customerId)}:mutate", null, body);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Searches for audience attributes that can be used to generate insights.</summary>
    /// <remarks>Body: GoogleAdsGoogleadsV26Services__ListAudienceInsightsAttributesRequest JSON as documented at developers.google.com/google-ads/api/rest/reference/rest/v26.</remarks>
    [HttpPost("customers/{customerId}/searchAudienceInsightsAttributes")]
    public async Task<IActionResult> SearchAudienceInsightsAttributesAsync([FromRoute] string customerId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] JsonObject? body)
    {
        var result = await _restService.SendAsync(customerId, "Customers.SearchAudienceInsightsAttributes", HttpMethod.Post, $"customers/{Uri.EscapeDataString(customerId)}:searchAudienceInsightsAttributes", null, body);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Removes text customization (formerly automatically created assets) from a campaign.</summary>
    /// <remarks>Body: GoogleAdsGoogleadsV26Services__RemoveCampaignAutomaticallyCreatedAssetRequest JSON as documented at developers.google.com/google-ads/api/rest/reference/rest/v26.</remarks>
    [HttpPost("customers/{customerId}/removeCampaignAutomaticallyCreatedAsset")]
    public async Task<IActionResult> RemoveCampaignAutomaticallyCreatedAssetAsync([FromRoute] string customerId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] JsonObject? body)
    {
        var result = await _restService.SendAsync(customerId, "Customers.RemoveCampaignAutomaticallyCreatedAsset", HttpMethod.Post, $"customers/{Uri.EscapeDataString(customerId)}:removeCampaignAutomaticallyCreatedAsset", null, body);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Returns a collection of targeting insights (e.g.</summary>
    /// <remarks>Body: GoogleAdsGoogleadsV26Services__GenerateSuggestedTargetingInsightsRequest JSON as documented at developers.google.com/google-ads/api/rest/reference/rest/v26.</remarks>
    [HttpPost("customers/{customerId}/generateSuggestedTargetingInsights")]
    public async Task<IActionResult> GenerateSuggestedTargetingInsightsAsync([FromRoute] string customerId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] JsonObject? body)
    {
        var result = await _restService.SendAsync(customerId, "Customers.GenerateSuggestedTargetingInsights", HttpMethod.Post, $"customers/{Uri.EscapeDataString(customerId)}:generateSuggestedTargetingInsights", null, body);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Returns insights for trending content on YouTube.</summary>
    /// <remarks>Body: GoogleAdsGoogleadsV26Services__GenerateTrendingInsightsRequest JSON as documented at developers.google.com/google-ads/api/rest/reference/rest/v26.</remarks>
    [HttpPost("customers/{customerId}/generateTrendingInsights")]
    public async Task<IActionResult> GenerateTrendingInsightsAsync([FromRoute] string customerId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] JsonObject? body)
    {
        var result = await _restService.SendAsync(customerId, "Customers.GenerateTrendingInsights", HttpMethod.Post, $"customers/{Uri.EscapeDataString(customerId)}:generateTrendingInsights", null, body);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Returns a list of suggested AdGroups and suggested modifications (text, match type) for the given keywords.</summary>
    /// <remarks>Body: GoogleAdsGoogleadsV26Services__GenerateAdGroupThemesRequest JSON as documented at developers.google.com/google-ads/api/rest/reference/rest/v26.</remarks>
    [HttpPost("customers/{customerId}/generateAdGroupThemes")]
    public async Task<IActionResult> GenerateAdGroupThemesAsync([FromRoute] string customerId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] JsonObject? body)
    {
        var result = await _restService.SendAsync(customerId, "Customers.GenerateAdGroupThemes", HttpMethod.Post, $"customers/{Uri.EscapeDataString(customerId)}:generateAdGroupThemes", null, body);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Creates a saved report that can be viewed in the Insights Finder tool.</summary>
    /// <remarks>Body: GoogleAdsGoogleadsV26Services__GenerateInsightsFinderReportRequest JSON as documented at developers.google.com/google-ads/api/rest/reference/rest/v26.</remarks>
    [HttpPost("customers/{customerId}/generateInsightsFinderReport")]
    public async Task<IActionResult> GenerateInsightsFinderReportAsync([FromRoute] string customerId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] JsonObject? body)
    {
        var result = await _restService.SendAsync(customerId, "Customers.GenerateInsightsFinderReport", HttpMethod.Post, $"customers/{Uri.EscapeDataString(customerId)}:generateInsightsFinderReport", null, body);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Processes the given click conversions.</summary>
    /// <remarks>Body: GoogleAdsGoogleadsV26Services__UploadClickConversionsRequest JSON as documented at developers.google.com/google-ads/api/rest/reference/rest/v26.</remarks>
    [HttpPost("customers/{customerId}/uploadClickConversions")]
    public async Task<IActionResult> UploadClickConversionsAsync([FromRoute] string customerId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] JsonObject? body)
    {
        var result = await _restService.SendAsync(customerId, "Customers.UploadClickConversions", HttpMethod.Post, $"customers/{Uri.EscapeDataString(customerId)}:uploadClickConversions", null, body);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Returns Identity Verification information.</summary>
    [HttpGet("customers/{customerId}/getIdentityVerification")]
    public async Task<IActionResult> GetIdentityVerificationAsync([FromRoute] string customerId)
    {
        var result = await _restService.SendAsync(customerId, "Customers.GetIdentityVerification", HttpMethod.Get, $"customers/{Uri.EscapeDataString(customerId)}/getIdentityVerification", null, null);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Returns potential reach metrics for targetable audiences.</summary>
    /// <remarks>Body: GoogleAdsGoogleadsV26Services__GenerateTargetingSuggestionMetricsRequest JSON as documented at developers.google.com/google-ads/api/rest/reference/rest/v26.</remarks>
    [HttpPost("customers/{customerId}/generateTargetingSuggestionMetrics")]
    public async Task<IActionResult> GenerateTargetingSuggestionMetricsAsync([FromRoute] string customerId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] JsonObject? body)
    {
        var result = await _restService.SendAsync(customerId, "Customers.GenerateTargetingSuggestionMetrics", HttpMethod.Post, $"customers/{Uri.EscapeDataString(customerId)}:generateTargetingSuggestionMetrics", null, body);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Returns BudgetOption suggestions.</summary>
    /// <remarks>Body: GoogleAdsGoogleadsV26Services__SuggestSmartCampaignBudgetOptionsRequest JSON as documented at developers.google.com/google-ads/api/rest/reference/rest/v26.</remarks>
    [HttpPost("customers/{customerId}/suggestSmartCampaignBudgetOptions")]
    public async Task<IActionResult> SuggestSmartCampaignBudgetOptionsAsync([FromRoute] string customerId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] JsonObject? body)
    {
        var result = await _restService.SendAsync(customerId, "Customers.SuggestSmartCampaignBudgetOptions", HttpMethod.Post, $"customers/{Uri.EscapeDataString(customerId)}:suggestSmartCampaignBudgetOptions", null, body);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Returns metrics (such as impressions, clicks, total cost) of a keyword forecast for the given campaign.</summary>
    /// <remarks>Body: GoogleAdsGoogleadsV26Services__GenerateKeywordForecastMetricsRequest JSON as documented at developers.google.com/google-ads/api/rest/reference/rest/v26.</remarks>
    [HttpPost("customers/{customerId}/generateKeywordForecastMetrics")]
    public async Task<IActionResult> GenerateKeywordForecastMetricsAsync([FromRoute] string customerId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] JsonObject? body)
    {
        var result = await _restService.SendAsync(customerId, "Customers.GenerateKeywordForecastMetrics", HttpMethod.Post, $"customers/{Uri.EscapeDataString(customerId)}:generateKeywordForecastMetrics", null, body);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Returns resource names of customers directly accessible by the user authenticating the call.</summary>
    [HttpGet("customers/listAccessibleCustomers")]
    public async Task<IActionResult> ListAccessibleCustomersAsync([FromQuery] string customerId)
    {
        if (string.IsNullOrEmpty(customerId)) return BadRequest("customerId (whose stored token is used) is required.");
        var result = await _restService.SendAsync(customerId, "Customers.ListAccessibleCustomers", HttpMethod.Get, $"customers:listAccessibleCustomers", null, null);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Processes the given call conversions.</summary>
    /// <remarks>Body: GoogleAdsGoogleadsV26Services__UploadCallConversionsRequest JSON as documented at developers.google.com/google-ads/api/rest/reference/rest/v26.</remarks>
    [HttpPost("customers/{customerId}/uploadCallConversions")]
    public async Task<IActionResult> UploadCallConversionsAsync([FromRoute] string customerId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] JsonObject? body)
    {
        var result = await _restService.SendAsync(customerId, "Customers.UploadCallConversions", HttpMethod.Post, $"customers/{Uri.EscapeDataString(customerId)}:uploadCallConversions", null, body);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Processes the given conversion adjustments.</summary>
    /// <remarks>Body: GoogleAdsGoogleadsV26Services__UploadConversionAdjustmentsRequest JSON as documented at developers.google.com/google-ads/api/rest/reference/rest/v26.</remarks>
    [HttpPost("customers/{customerId}/uploadConversionAdjustments")]
    public async Task<IActionResult> UploadConversionAdjustmentsAsync([FromRoute] string customerId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] JsonObject? body)
    {
        var result = await _restService.SendAsync(customerId, "Customers.UploadConversionAdjustments", HttpMethod.Post, $"customers/{Uri.EscapeDataString(customerId)}:uploadConversionAdjustments", null, body);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Returns a collection of audience attributes using generative AI based on the provided audience description.</summary>
    /// <remarks>Body: GoogleAdsGoogleadsV26Services__GenerateAudienceDefinitionRequest JSON as documented at developers.google.com/google-ads/api/rest/reference/rest/v26.</remarks>
    [HttpPost("customers/{customerId}/generateAudienceDefinition")]
    public async Task<IActionResult> GenerateAudienceDefinitionAsync([FromRoute] string customerId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] JsonObject? body)
    {
        var result = await _restService.SendAsync(customerId, "Customers.GenerateAudienceDefinition", HttpMethod.Post, $"customers/{Uri.EscapeDataString(customerId)}:generateAudienceDefinition", null, body);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Suggests a Smart campaign ad compatible with the Ad family of resources, based on data points such as targeting and the business to advertise.</summary>
    /// <remarks>Body: GoogleAdsGoogleadsV26Services__SuggestSmartCampaignAdRequest JSON as documented at developers.google.com/google-ads/api/rest/reference/rest/v26.</remarks>
    [HttpPost("customers/{customerId}/suggestSmartCampaignAd")]
    public async Task<IActionResult> SuggestSmartCampaignAdAsync([FromRoute] string customerId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] JsonObject? body)
    {
        var result = await _restService.SendAsync(customerId, "Customers.SuggestSmartCampaignAd", HttpMethod.Post, $"customers/{Uri.EscapeDataString(customerId)}:suggestSmartCampaignAd", null, body);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Returns a list of keyword ideas.</summary>
    /// <remarks>Body: GoogleAdsGoogleadsV26Services__GenerateKeywordIdeasRequest JSON as documented at developers.google.com/google-ads/api/rest/reference/rest/v26.</remarks>
    [HttpPost("customers/{customerId}/generateKeywordIdeas")]
    public async Task<IActionResult> GenerateKeywordIdeasAsync([FromRoute] string customerId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] JsonObject? body)
    {
        var result = await _restService.SendAsync(customerId, "Customers.GenerateKeywordIdeas", HttpMethod.Post, $"customers/{Uri.EscapeDataString(customerId)}:generateKeywordIdeas", null, body);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Returns Travel Asset suggestions.</summary>
    /// <remarks>Body: GoogleAdsGoogleadsV26Services__SuggestTravelAssetsRequest JSON as documented at developers.google.com/google-ads/api/rest/reference/rest/v26.</remarks>
    [HttpPost("customers/{customerId}/suggestTravelAssets")]
    public async Task<IActionResult> SuggestTravelAssetsAsync([FromRoute] string customerId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] JsonObject? body)
    {
        var result = await _restService.SendAsync(customerId, "Customers.SuggestTravelAssets", HttpMethod.Post, $"customers/{Uri.EscapeDataString(customerId)}:suggestTravelAssets", null, body);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Generates a reach forecast for a given targeting / product mix.</summary>
    /// <remarks>Body: GoogleAdsGoogleadsV26Services__GenerateReachForecastRequest JSON as documented at developers.google.com/google-ads/api/rest/reference/rest/v26.</remarks>
    [HttpPost("customers/{customerId}/generateReachForecast")]
    public async Task<IActionResult> GenerateReachForecastAsync([FromRoute] string customerId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] JsonObject? body)
    {
        var result = await _restService.SendAsync(customerId, "Customers.GenerateReachForecast", HttpMethod.Post, $"customers/{Uri.EscapeDataString(customerId)}:generateReachForecast", null, body);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Suggests keyword themes to advertise on.</summary>
    /// <remarks>Body: GoogleAdsGoogleadsV26Services__SuggestKeywordThemesRequest JSON as documented at developers.google.com/google-ads/api/rest/reference/rest/v26.</remarks>
    [HttpPost("customers/{customerId}/suggestKeywordThemes")]
    public async Task<IActionResult> SuggestKeywordThemesAsync([FromRoute] string customerId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] JsonObject? body)
    {
        var result = await _restService.SendAsync(customerId, "Customers.SuggestKeywordThemes", HttpMethod.Post, $"customers/{Uri.EscapeDataString(customerId)}:suggestKeywordThemes", null, body);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Returns a collection of attributes that are represented in an audience of interest, with metrics that compare each attribute's share of the audience with its share of a baseline...</summary>
    /// <remarks>Body: GoogleAdsGoogleadsV26Services__GenerateAudienceCompositionInsightsRequest JSON as documented at developers.google.com/google-ads/api/rest/reference/rest/v26.</remarks>
    [HttpPost("customers/{customerId}/generateAudienceCompositionInsights")]
    public async Task<IActionResult> GenerateAudienceCompositionInsightsAsync([FromRoute] string customerId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] JsonObject? body)
    {
        var result = await _restService.SendAsync(customerId, "Customers.GenerateAudienceCompositionInsights", HttpMethod.Post, $"customers/{Uri.EscapeDataString(customerId)}:generateAudienceCompositionInsights", null, body);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Starts Identity Verification for a given verification program type.</summary>
    /// <remarks>Body: GoogleAdsGoogleadsV26Services__StartIdentityVerificationRequest JSON as documented at developers.google.com/google-ads/api/rest/reference/rest/v26.</remarks>
    [HttpPost("customers/{customerId}/startIdentityVerification")]
    public async Task<IActionResult> StartIdentityVerificationAsync([FromRoute] string customerId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] JsonObject? body)
    {
        var result = await _restService.SendAsync(customerId, "Customers.StartIdentityVerification", HttpMethod.Post, $"customers/{Uri.EscapeDataString(customerId)}:startIdentityVerification", null, body);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Returns a collection of audience attributes along with estimates of the overlap between their potential YouTube reach and that of a given input attribute.</summary>
    /// <remarks>Body: GoogleAdsGoogleadsV26Services__GenerateAudienceOverlapInsightsRequest JSON as documented at developers.google.com/google-ads/api/rest/reference/rest/v26.</remarks>
    [HttpPost("customers/{customerId}/generateAudienceOverlapInsights")]
    public async Task<IActionResult> GenerateAudienceOverlapInsightsAsync([FromRoute] string customerId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] JsonObject? body)
    {
        var result = await _restService.SendAsync(customerId, "Customers.GenerateAudienceOverlapInsights", HttpMethod.Post, $"customers/{Uri.EscapeDataString(customerId)}:generateAudienceOverlapInsights", null, body);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Returns the Local Services eligibility status and valid categories for a given Customer and GBP Listing ID.</summary>
    /// <remarks>Body: GoogleAdsGoogleadsV26Services__CheckLocalServicesEligibilityRequest JSON as documented at developers.google.com/google-ads/api/rest/reference/rest/v26.</remarks>
    [HttpPost("customers/{customerId}/checkLocalServicesEligibility")]
    public async Task<IActionResult> CheckLocalServicesEligibilityAsync([FromRoute] string customerId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] JsonObject? body)
    {
        var result = await _restService.SendAsync(customerId, "Customers.CheckLocalServicesEligibility", HttpMethod.Post, $"customers/{Uri.EscapeDataString(customerId)}:checkLocalServicesEligibility", null, body);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Returns YouTube advertisement metrics for the given client against industry benchmarks.</summary>
    /// <remarks>Body: GoogleAdsGoogleadsV26Services__GenerateBenchmarksMetricsRequest JSON as documented at developers.google.com/google-ads/api/rest/reference/rest/v26.</remarks>
    [HttpPost("customers/{customerId}/generateBenchmarksMetrics")]
    public async Task<IActionResult> GenerateBenchmarksMetricsAsync([FromRoute] string customerId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] JsonObject? body)
    {
        var result = await _restService.SendAsync(customerId, "Customers.GenerateBenchmarksMetrics", HttpMethod.Post, $"customers/{Uri.EscapeDataString(customerId)}:generateBenchmarksMetrics", null, body);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Creates a new client under manager.</summary>
    /// <remarks>Body: GoogleAdsGoogleadsV26Services__CreateCustomerClientRequest JSON as documented at developers.google.com/google-ads/api/rest/reference/rest/v26.</remarks>
    [HttpPost("customers/{customerId}/createCustomerClient")]
    public async Task<IActionResult> CreateCustomerClientAsync([FromRoute] string customerId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] JsonObject? body)
    {
        var result = await _restService.SendAsync(customerId, "Customers.CreateCustomerClient", HttpMethod.Post, $"customers/{Uri.EscapeDataString(customerId)}:createCustomerClient", null, body);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Rpc to return a list of matching brands based on a prefix for this customer.</summary>
    /// <remarks>Body: GoogleAdsGoogleadsV26Services__SuggestBrandsRequest JSON as documented at developers.google.com/google-ads/api/rest/reference/rest/v26.</remarks>
    [HttpPost("customers/{customerId}/suggestBrands")]
    public async Task<IActionResult> SuggestBrandsAsync([FromRoute] string customerId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] JsonObject? body)
    {
        var result = await _restService.SendAsync(customerId, "Customers.SuggestBrands", HttpMethod.Post, $"customers/{Uri.EscapeDataString(customerId)}:suggestBrands", null, body);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Returns a list of keyword historical metrics.</summary>
    /// <remarks>Body: GoogleAdsGoogleadsV26Services__GenerateKeywordHistoricalMetricsRequest JSON as documented at developers.google.com/google-ads/api/rest/reference/rest/v26.</remarks>
    [HttpPost("customers/{customerId}/generateKeywordHistoricalMetrics")]
    public async Task<IActionResult> GenerateKeywordHistoricalMetricsAsync([FromRoute] string customerId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] JsonObject? body)
    {
        var result = await _restService.SendAsync(customerId, "Customers.GenerateKeywordHistoricalMetrics", HttpMethod.Post, $"customers/{Uri.EscapeDataString(customerId)}:generateKeywordHistoricalMetrics", null, body);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Returns the requested Shareable Preview.</summary>
    /// <remarks>Body: GoogleAdsGoogleadsV26Services__GenerateShareablePreviewsRequest JSON as documented at developers.google.com/google-ads/api/rest/reference/rest/v26.</remarks>
    [HttpPost("customers/{customerId}/generateShareablePreviews")]
    public async Task<IActionResult> GenerateShareablePreviewsAsync([FromRoute] string customerId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] JsonObject? body)
    {
        var result = await _restService.SendAsync(customerId, "Customers.GenerateShareablePreviews", HttpMethod.Post, $"customers/{Uri.EscapeDataString(customerId)}:generateShareablePreviews", null, body);
        return result.Success ? Ok(result) : BadRequest(result);
    }

}
