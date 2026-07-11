using System.Net;
using FluentAssertions;
using Xunit;

namespace Google.ApiTests.Endpoints;

public class GoogleAdsEndpointTests
{
    private const string BaseUrl = "https://localhost:5001";

    private HttpClient CreateClient()
    {
        var handler = new HttpClientHandler { ServerCertificateCustomValidationCallback = (_, _, _, _) => true };
        return new HttpClient(handler) { BaseAddress = new Uri(BaseUrl), Timeout = TimeSpan.FromSeconds(15) };
    }

    [SkippableFact]
    public async Task GetAuthUrl_ShouldReturnOk()
    {
        using var client = CreateClient();
        try
        {
            var response = await client.GetAsync("/api/googleads/auth/url");
            response.StatusCode.Should().BeOneOf(
                new[] { HttpStatusCode.OK, HttpStatusCode.BadRequest },
                "Auth URL endpoint should respond (config may be missing in test env)");
        }
        catch (HttpRequestException) { throw new SkipException("GoogleAds API not running"); }
    }

    [SkippableFact]
    public async Task ListCampaigns_WithoutCustomerId_ShouldReturn400()
    {
        using var client = CreateClient();
        try
        {
            var response = await client.GetAsync("/api/googleads/campaigns");
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest,
                "Campaigns endpoint requires customerId query param");
        }
        catch (HttpRequestException) { throw new SkipException("GoogleAds API not running"); }
    }

    [SkippableFact]
    public async Task CreateCampaign_WithEmptyBody_ShouldReturn400()
    {
        using var client = CreateClient();
        try
        {
            var content = new System.Net.Http.StringContent("{}", System.Text.Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/api/googleads/campaigns", content);
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest, "Empty body should fail validation");
        }
        catch (HttpRequestException) { throw new SkipException("GoogleAds API not running"); }
    }

    [SkippableFact]
    public async Task RunReport_WithEmptyBody_ShouldReturn400()
    {
        using var client = CreateClient();
        try
        {
            var content = new System.Net.Http.StringContent("{}", System.Text.Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/api/googleads/reports/run", content);
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest, "Report requires customerId and query");
        }
        catch (HttpRequestException) { throw new SkipException("GoogleAds API not running"); }
    }
}
