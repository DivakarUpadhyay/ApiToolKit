namespace ApiToolKit.Application.Services.GoogleAds;

/// <summary>
/// Single place for the Google Ads API version. Google sunsets each version about a year after release,
/// so bump this when a new major version ships (see developers.google.com/google-ads/api/docs/sunset-dates).
/// </summary>
public static class GoogleAdsApiVersion
{
    public const string Current = "v26";
    public const string BaseUrl = "https://googleads.googleapis.com/" + Current;
}
