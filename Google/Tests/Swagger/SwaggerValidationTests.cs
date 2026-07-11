using System.Net;
using FluentAssertions;
using Google.ApiTests.Helpers;
using Newtonsoft.Json.Linq;
using Xunit;

namespace Google.ApiTests.Swagger;

/// <summary>
/// Swagger documentation validation tests.
/// These tests verify that each Google API's Swagger UI is available and correctly documents endpoints.
/// Run the target API first before executing these tests.
/// </summary>
public class SwaggerValidationTests
{
    private static readonly Dictionary<string, string> ApiUrls = new()
    {
        ["GoogleAds"]  = "https://localhost:5001",
        ["YouTube"]    = "https://localhost:5002",
        ["Workspace"]  = "https://localhost:5003",
        ["Maps"]       = "https://localhost:5004",
        ["Analytics"]  = "https://localhost:5005",
        ["Cloud"]      = "https://localhost:5006",
        ["BigQuery"]   = "https://localhost:5007",
        ["Search"]     = "https://localhost:5008",
        ["Identity"]   = "https://localhost:5009",
        ["Firebase"]   = "https://localhost:5010",
    };

    [SkippableTheory]
    [InlineData("GoogleAds",  "https://localhost:5001")]
    [InlineData("YouTube",    "https://localhost:5002")]
    [InlineData("Workspace",  "https://localhost:5003")]
    [InlineData("Maps",       "https://localhost:5004")]
    [InlineData("Analytics",  "https://localhost:5005")]
    [InlineData("Cloud",      "https://localhost:5006")]
    [InlineData("BigQuery",   "https://localhost:5007")]
    [InlineData("Search",     "https://localhost:5008")]
    [InlineData("Identity",   "https://localhost:5009")]
    [InlineData("Firebase",   "https://localhost:5010")]
    public async Task SwaggerJson_ShouldBeAvailable(string apiName, string baseUrl)
    {
        var handler = new HttpClientHandler { ServerCertificateCustomValidationCallback = (_, _, _, _) => true };
        using var client = new HttpClient(handler) { BaseAddress = new Uri(baseUrl), Timeout = TimeSpan.FromSeconds(10) };

        try
        {
            var response = await client.GetAsync("/swagger/v1/swagger.json");
            response.StatusCode.Should().Be(HttpStatusCode.OK, $"Swagger JSON should be available for {apiName}. Is the API running on {baseUrl}?");
        }
        catch (HttpRequestException)
        {
            // API not running — skip gracefully
            throw new SkipException($"{apiName} API is not running on {baseUrl}. Start it first with: dotnet run --project {apiName}/ApiToolKit.Web");
        }
    }

    [SkippableTheory]
    [InlineData("GoogleAds",  "https://localhost:5001", new[] { "api/googleads/auth", "api/googleads/campaigns", "api/googleads/adgroups", "api/googleads/ads", "api/googleads/keywords" })]
    [InlineData("YouTube",    "https://localhost:5002", new[] { "api/youtube/auth", "api/youtube/channels", "api/youtube/videos", "api/youtube/playlists" })]
    [InlineData("Workspace",  "https://localhost:5003", new[] { "api/workspace/auth", "api/workspace/gmail/messages", "api/workspace/drive/files", "api/workspace/calendar/events" })]
    [InlineData("Maps",       "https://localhost:5004", new[] { "api/maps/geocode", "api/maps/directions", "api/maps/places" })]
    [InlineData("Analytics",  "https://localhost:5005", new[] { "api/analytics/auth", "api/analytics/reports" })]
    [InlineData("Cloud",      "https://localhost:5006", new[] { "api/cloud/vision", "api/cloud/speech", "api/cloud/language", "api/cloud/translation" })]
    [InlineData("BigQuery",   "https://localhost:5007", new[] { "api/bigquery/auth", "api/bigquery/datasets", "api/bigquery/jobs" })]
    [InlineData("Search",     "https://localhost:5008", new[] { "api/search/custom", "api/search/knowledge-graph" })]
    [InlineData("Identity",   "https://localhost:5009", new[] { "api/identity/oauth", "api/identity/people", "api/identity/toolkit" })]
    [InlineData("Firebase",   "https://localhost:5010", new[] { "api/firebase/auth", "api/firebase/fcm", "api/firebase/users" })]
    public async Task SwaggerJson_ShouldContainExpectedPaths(string apiName, string baseUrl, string[] expectedPaths)
    {
        var handler = new HttpClientHandler { ServerCertificateCustomValidationCallback = (_, _, _, _) => true };
        using var client = new HttpClient(handler) { BaseAddress = new Uri(baseUrl), Timeout = TimeSpan.FromSeconds(10) };

        string swaggerJson;
        try
        {
            var response = await client.GetAsync("/swagger/v1/swagger.json");
            if (!response.IsSuccessStatusCode)
            {
                throw new SkipException($"{apiName} API not running on {baseUrl}");
            }
            swaggerJson = await response.Content.ReadAsStringAsync();
        }
        catch (HttpRequestException)
        {
            throw new SkipException($"{apiName} API not running on {baseUrl}");
        }

        var swagger = JObject.Parse(swaggerJson);
        var paths = swagger["paths"]?.ToObject<JObject>();
        paths.Should().NotBeNull($"{apiName} Swagger should have paths defined");

        foreach (var expectedPath in expectedPaths)
        {
            var pathKey = "/" + expectedPath;
            // Check that at least one path starts with the expected prefix
            var hasPath = paths!.Properties().Any(p => p.Name.StartsWith(pathKey, StringComparison.OrdinalIgnoreCase));
            hasPath.Should().BeTrue($"{apiName} should have endpoints under '{pathKey}'");
        }
    }
}
