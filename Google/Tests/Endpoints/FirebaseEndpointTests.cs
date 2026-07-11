using System.Net;
using FluentAssertions;
using Xunit;

namespace Google.ApiTests.Endpoints;

public class FirebaseEndpointTests
{
    private const string BaseUrl = "https://localhost:5010";

    private HttpClient CreateClient()
    {
        var handler = new HttpClientHandler { ServerCertificateCustomValidationCallback = (_, _, _, _) => true };
        return new HttpClient(handler) { BaseAddress = new Uri(BaseUrl), Timeout = TimeSpan.FromSeconds(15) };
    }

    [SkippableFact]
    public async Task SignUp_WithEmptyBody_ShouldReturn400()
    {
        using var client = CreateClient();
        try
        {
            var content = new System.Net.Http.StringContent("{}", System.Text.Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/api/firebase/users/signup", content);
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest, "SignUp requires email and password");
        }
        catch (HttpRequestException) { throw new SkipException("Firebase API not running"); }
    }

    [SkippableFact]
    public async Task SendFcmToToken_WithEmptyBody_ShouldReturn400()
    {
        using var client = CreateClient();
        try
        {
            var content = new System.Net.Http.StringContent("{}", System.Text.Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/api/firebase/fcm/send/token", content);
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest, "FCM send requires token, title, and body");
        }
        catch (HttpRequestException) { throw new SkipException("Firebase API not running"); }
    }
}
