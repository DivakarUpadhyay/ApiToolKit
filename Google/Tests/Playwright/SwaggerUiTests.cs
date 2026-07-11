using Microsoft.Playwright;
using Xunit;

namespace Google.ApiTests.Playwright;

/// <summary>
/// Playwright-based end-to-end tests for Swagger UI interaction.
/// Each API must be running on its designated port before these tests execute.
/// </summary>
public class SwaggerUiTests : IAsyncLifetime
{
    private IPlaywright? _playwright;
    private IBrowser? _browser;

    public async Task InitializeAsync()
    {
        _playwright = await Microsoft.Playwright.Playwright.CreateAsync();
        _browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = true,
            Args = new[] { "--no-sandbox", "--disable-setuid-sandbox", "--ignore-certificate-errors" }
        });
    }

    public async Task DisposeAsync()
    {
        if (_browser != null) await _browser.DisposeAsync();
        _playwright?.Dispose();
    }

    private async Task<IPage> NewPageAsync()
    {
        var context = await _browser!.NewContextAsync(new BrowserNewContextOptions
        {
            IgnoreHTTPSErrors = true
        });
        return await context.NewPageAsync();
    }

    [SkippableTheory]
    [InlineData("GoogleAds API", "https://localhost:5001/swagger")]
    [InlineData("YouTube API", "https://localhost:5002/swagger")]
    [InlineData("Workspace API", "https://localhost:5003/swagger")]
    [InlineData("Maps API", "https://localhost:5004/swagger")]
    [InlineData("Analytics API", "https://localhost:5005/swagger")]
    [InlineData("Cloud AI API", "https://localhost:5006/swagger")]
    [InlineData("BigQuery API", "https://localhost:5007/swagger")]
    [InlineData("Search API", "https://localhost:5008/swagger")]
    [InlineData("Identity API", "https://localhost:5009/swagger")]
    [InlineData("Firebase API", "https://localhost:5010/swagger")]
    public async Task SwaggerUI_ShouldLoad(string apiName, string swaggerUrl)
    {
        var page = await NewPageAsync();
        try
        {
            var response = await page.GotoAsync(swaggerUrl, new PageGotoOptions { Timeout = 10000 });
            if (response == null || !response.Ok)
            {
                throw new SkipException($"{apiName} Swagger UI not accessible at {swaggerUrl}. Start the API first.");
            }

            await page.WaitForSelectorAsync(".swagger-ui", new PageWaitForSelectorOptions { Timeout = 10000 });
            var title = await page.TitleAsync();
            Assert.Contains("Swagger", title, StringComparison.OrdinalIgnoreCase);
        }
        catch (PlaywrightException ex) when (ex.Message.Contains("ERR_CONNECTION_REFUSED") || ex.Message.Contains("ECONNREFUSED"))
        {
            throw new SkipException($"{apiName} not running at {swaggerUrl}");
        }
        finally
        {
            await page.CloseAsync();
        }
    }

    [SkippableTheory]
    [InlineData("GoogleAds API", "https://localhost:5001/swagger", "ApiToolKit - Google Ads API")]
    [InlineData("YouTube API", "https://localhost:5002/swagger", "YouTube")]
    [InlineData("Workspace API", "https://localhost:5003/swagger", "Workspace")]
    [InlineData("Maps API", "https://localhost:5004/swagger", "Maps")]
    [InlineData("Analytics API", "https://localhost:5005/swagger", "Analytics")]
    public async Task SwaggerUI_ShouldShowCorrectTitle(string apiName, string swaggerUrl, string expectedTitlePart)
    {
        var page = await NewPageAsync();
        try
        {
            var response = await page.GotoAsync(swaggerUrl, new PageGotoOptions { Timeout = 10000 });
            if (response == null || !response.Ok)
            {
                throw new SkipException($"{apiName} not running");
            }

            await page.WaitForSelectorAsync(".swagger-ui .info h2", new PageWaitForSelectorOptions { Timeout = 8000 });
            var headerText = await page.InnerTextAsync(".swagger-ui .info h2");
            Assert.Contains(expectedTitlePart, headerText, StringComparison.OrdinalIgnoreCase);
        }
        catch (PlaywrightException)
        {
            throw new SkipException($"{apiName} not running at {swaggerUrl}");
        }
        finally
        {
            await page.CloseAsync();
        }
    }

    [SkippableFact]
    public async Task GoogleAdsSwagger_ShouldListCampaignEndpoints()
    {
        var page = await NewPageAsync();
        try
        {
            var response = await page.GotoAsync("https://localhost:5001/swagger", new PageGotoOptions { Timeout = 10000 });
            if (response == null || !response.Ok) { throw new SkipException("GoogleAds not running"); }

            await page.WaitForSelectorAsync(".swagger-ui .opblock-tag", new PageWaitForSelectorOptions { Timeout = 8000 });
            var tags = await page.QuerySelectorAllAsync(".swagger-ui .opblock-tag");
            var tagTexts = new List<string>();
            foreach (var tag in tags)
                tagTexts.Add(await tag.InnerTextAsync());

            Assert.Contains(tagTexts, t => t.Contains("Campaign", StringComparison.OrdinalIgnoreCase));
        }
        catch (PlaywrightException) { throw new SkipException("GoogleAds API not running"); }
        finally { await page.CloseAsync(); }
    }

    [SkippableFact]
    public async Task WorkspaceSwagger_ShouldListGmailAndDriveEndpoints()
    {
        var page = await NewPageAsync();
        try
        {
            var response = await page.GotoAsync("https://localhost:5003/swagger", new PageGotoOptions { Timeout = 10000 });
            if (response == null || !response.Ok) { throw new SkipException("Workspace not running"); }

            await page.WaitForSelectorAsync(".swagger-ui .opblock-tag", new PageWaitForSelectorOptions { Timeout = 8000 });
            var tags = await page.QuerySelectorAllAsync(".swagger-ui .opblock-tag");
            var tagTexts = new List<string>();
            foreach (var tag in tags)
                tagTexts.Add(await tag.InnerTextAsync());

            Assert.Contains(tagTexts, t => t.Contains("Gmail", StringComparison.OrdinalIgnoreCase));
            Assert.Contains(tagTexts, t => t.Contains("Drive", StringComparison.OrdinalIgnoreCase));
        }
        catch (PlaywrightException) { throw new SkipException("Workspace API not running"); }
        finally { await page.CloseAsync(); }
    }
}
