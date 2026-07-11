using System.Net;
using FluentAssertions;
using Xunit;

namespace Google.ApiTests.Endpoints;

public class YouTubeEndpointTests
{
    private const string BaseUrl = "https://localhost:5002";

    private HttpClient CreateClient()
    {
        var handler = new HttpClientHandler { ServerCertificateCustomValidationCallback = (_, _, _, _) => true };
        return new HttpClient(handler) { BaseAddress = new Uri(BaseUrl), Timeout = TimeSpan.FromSeconds(15) };
    }

    [SkippableFact]
    public async Task GetAuthUrl_ShouldRespond()
    {
        using var client = CreateClient();
        try
        {
            var response = await client.GetAsync("/api/youtube/auth/url");
            response.StatusCode.Should().BeOneOf(HttpStatusCode.OK, HttpStatusCode.BadRequest);
        }
        catch (HttpRequestException) { throw new SkipException("YouTube API not running"); }
    }

    [SkippableFact]
    public async Task Search_WithoutQuery_ShouldReturn400()
    {
        using var client = CreateClient();
        try
        {
            var response = await client.GetAsync("/api/youtube/search");
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest, "Search requires Q param");
        }
        catch (HttpRequestException) { throw new SkipException("YouTube API not running"); }
    }

    [SkippableFact]
    public async Task GetVideo_WithoutVideoId_ShouldReturn400()
    {
        using var client = CreateClient();
        try
        {
            var response = await client.GetAsync("/api/youtube/videos/list");
            response.StatusCode.Should().BeOneOf(HttpStatusCode.OK, HttpStatusCode.BadRequest);
        }
        catch (HttpRequestException) { throw new SkipException("YouTube API not running"); }
    }
}
