using System.Net;
using FluentAssertions;
using Xunit;

namespace Google.ApiTests.Endpoints;

public class BusinessProfileEndpointTests
{
    private const string BaseUrl = "https://localhost:5012";

    private HttpClient CreateClient()
    {
        var handler = new HttpClientHandler { ServerCertificateCustomValidationCallback = (_, _, _, _) => true };
        return new HttpClient(handler) { BaseAddress = new Uri(BaseUrl), Timeout = TimeSpan.FromSeconds(15) };
    }

    [SkippableFact]
    public async Task AuthUrl_WithoutAccountKey_ShouldReturn400()
    {
        using var client = CreateClient();
        try
        {
            var response = await client.GetAsync("/api/businessprofile/auth/url");
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest, "accountKey is required");
        }
        catch (HttpRequestException) { throw new SkipException("BusinessProfile API not running"); }
    }

    [SkippableFact]
    public async Task AuthUrl_WithAccountKey_ShouldReturnGoogleConsentUrl()
    {
        using var client = CreateClient();
        try
        {
            var response = await client.GetAsync("/api/businessprofile/auth/url?accountKey=test-account");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var body = await response.Content.ReadAsStringAsync();
            body.Should().Contain("accounts.google.com/o/oauth2/v2/auth");
        }
        catch (HttpRequestException) { throw new SkipException("BusinessProfile API not running"); }
    }

    [SkippableFact]
    public async Task ListAccounts_WithoutAccountKey_ShouldReturn400()
    {
        using var client = CreateClient();
        try
        {
            var response = await client.GetAsync("/api/businessprofile/accounts");
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest, "accountKey is required");
        }
        catch (HttpRequestException) { throw new SkipException("BusinessProfile API not running"); }
    }
}
