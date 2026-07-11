using System.Net;
using FluentAssertions;
using Xunit;

namespace Google.ApiTests.Endpoints;

public class MapsEndpointTests
{
    private const string BaseUrl = "https://localhost:5004";

    private HttpClient CreateClient()
    {
        var handler = new HttpClientHandler { ServerCertificateCustomValidationCallback = (_, _, _, _) => true };
        return new HttpClient(handler) { BaseAddress = new Uri(BaseUrl), Timeout = TimeSpan.FromSeconds(15) };
    }

    [SkippableFact]
    public async Task Geocode_WithoutAddress_ShouldReturn400()
    {
        using var client = CreateClient();
        try
        {
            var response = await client.GetAsync("/api/maps/geocode/address");
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest, "Geocode requires address param");
        }
        catch (HttpRequestException) { throw new SkipException("Maps API not running"); }
    }

    [SkippableFact]
    public async Task Directions_WithoutOriginDest_ShouldReturn400()
    {
        using var client = CreateClient();
        try
        {
            var response = await client.GetAsync("/api/maps/directions");
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest, "Directions requires origin and destination");
        }
        catch (HttpRequestException) { throw new SkipException("Maps API not running"); }
    }

    [SkippableFact]
    public async Task PlacesNearby_WithoutLatLng_ShouldReturn400()
    {
        using var client = CreateClient();
        try
        {
            var response = await client.GetAsync("/api/maps/places/nearby");
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest, "Nearby search requires lat/lng");
        }
        catch (HttpRequestException) { throw new SkipException("Maps API not running"); }
    }
}
