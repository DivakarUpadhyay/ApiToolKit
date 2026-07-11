using Microsoft.Playwright;

namespace Google.ApiTests.Helpers;

public static class PlaywrightSetup
{
    public static async Task<IBrowser> CreateBrowserAsync()
    {
        var playwright = await Microsoft.Playwright.Playwright.CreateAsync();
        return await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = true,
            Args = new[] { "--no-sandbox", "--disable-setuid-sandbox" }
        });
    }
}
