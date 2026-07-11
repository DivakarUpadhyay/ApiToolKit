using System.Net;
using FluentAssertions;
using Xunit;

namespace Google.ApiTests.Endpoints;

public class SearchEndpointTests
{
    private const string BaseUrl = "https://localhost:5008";

    private HttpClient CreateClient()
    {
        var handler = new HttpClientHandler { ServerCertificateCustomValidationCallback = (_, _, _, _) => true };
        return new HttpClient(handler) { BaseAddress = new Uri(BaseUrl), Timeout = TimeSpan.FromSeconds(15) };
    }

    [SkippableFact]
    public async Task CustomSearch_WithoutQuery_ShouldReturn400()
    {
        using var client = CreateClient();
        try
        {
            var response = await client.GetAsync("/api/search/custom");
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest, "Search requires Q param");
        }
        catch (HttpRequestException) { throw new SkipException("Search API not running"); }
    }

    [SkippableFact]
    public async Task KnowledgeGraph_WithoutQuery_ShouldReturn400()
    {
        using var client = CreateClient();
        try
        {
            var response = await client.GetAsync("/api/search/knowledge-graph");
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest, "Knowledge Graph requires query param");
        }
        catch (HttpRequestException) { throw new SkipException("Search API not running"); }
    }
}
