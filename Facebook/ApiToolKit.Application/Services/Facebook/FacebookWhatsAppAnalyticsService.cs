using ApiToolKit.Application.DTOs.Facebook.WhatsAppPlatform;
using ApiToolKit.Application.Interfaces.Services.Facebook;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.Facebook;


namespace ApiToolKit.Application.Services.Facebook;

public class FacebookWhatsAppAnalyticsService : WhatsAppServiceBase, IFacebookWhatsAppAnalyticsService
{
    private static string ListMod(string name, IEnumerable<string>? values) => WhatsAppFieldModifiers.List(name, values);

    public FacebookWhatsAppAnalyticsService(IWhatsAppApiClient api, IFacebookWhatsAppRepository repo) : base(api, repo) { }

    // pricing_analytics and call_analytics are WABA *fields* with modifiers.

    public Task<Result<object>> GetPricingAnalyticsAsync(WaPricingAnalyticsRequest r)
    {
        var field = $"pricing_analytics.start({r.Start}).end({r.End}).granularity({r.Granularity})"
                    + ListMod("phone_numbers", r.PhoneNumbers)
                    + ListMod("country_codes", r.CountryCodes)
                    + ListMod("metric_types", r.MetricTypes)
                    + ListMod("pricing_types", r.PricingTypes)
                    + ListMod("pricing_categories", r.PricingCategories)
                    + ListMod("dimensions", r.Dimensions);
        return RunAsync("GetPricingAnalytics", () => Api.GetAsync<object>($"/{r.WabaId}", r.AccessToken,
            new Dictionary<string, string> { ["fields"] = field }), r);
    }

    public Task<Result<object>> GetCallAnalyticsAsync(WaCallAnalyticsRequest r)
    {
        var field = $"call_analytics.start({r.Start}).end({r.End}).granularity({r.Granularity})"
                    + ListMod("phone_numbers", r.PhoneNumbers)
                    + ListMod("country_codes", r.CountryCodes)
                    + ListMod("directions", r.Directions)
                    + ListMod("dimensions", r.Dimensions)
                    + ListMod("metric_types", r.MetricTypes);
        return RunAsync("GetCallAnalytics", () => Api.GetAsync<object>($"/{r.WabaId}", r.AccessToken,
            new Dictionary<string, string> { ["fields"] = field }), r);
    }

    // template_analytics, template_group_analytics and group_analytics are *edges*.

    public Task<Result<object>> GetTemplateAnalyticsAsync(WaTemplateAnalyticsRequest r)
    {
        if (r.TemplateIds.Count is 0 or > 10)
            return Task.FromResult(Result<object>.Fail("Provide between 1 and 10 template IDs."));

        var query = new Dictionary<string, string>
        {
            ["start"] = r.Start,
            ["end"] = r.End,
            ["granularity"] = "DAILY",
            ["template_ids"] = WhatsAppQuery.JsonArray(r.TemplateIds)
        };
        if (r.MetricTypes is { Count: > 0 }) query["metric_types"] = WhatsAppQuery.JsonArray(r.MetricTypes);
        if (!string.IsNullOrEmpty(r.ProductType)) query["product_type"] = r.ProductType;
        if (r.UseWabaTimezone.HasValue) query["use_waba_timezone"] = r.UseWabaTimezone.Value ? "true" : "false";
        return RunAsync("GetTemplateAnalytics", () => Api.GetAsync<object>($"/{r.WabaId}/template_analytics", r.AccessToken, query), r);
    }

    public Task<Result<object>> GetTemplateGroupAnalyticsAsync(WaTemplateGroupAnalyticsRequest r)
    {
        if (r.TemplateGroupIds.Count is 0 or > 10)
            return Task.FromResult(Result<object>.Fail("Provide between 1 and 10 template group IDs."));

        var query = new Dictionary<string, string>
        {
            ["start"] = r.Start,
            ["end"] = r.End,
            ["granularity"] = "daily",
            ["template_group_ids"] = WhatsAppQuery.JsonArray(r.TemplateGroupIds)
        };
        if (r.MetricTypes is { Count: > 0 }) query["metric_types"] = WhatsAppQuery.JsonArray(r.MetricTypes);
        if (r.UseWabaTimezone.HasValue) query["use_waba_timezone"] = r.UseWabaTimezone.Value ? "true" : "false";
        return RunAsync("GetTemplateGroupAnalytics", () => Api.GetAsync<object>($"/{r.WabaId}/template_group_analytics", r.AccessToken, query), r);
    }

    public Task<Result<object>> GetGroupAnalyticsAsync(WaGroupAnalyticsRequest r)
    {
        var query = new Dictionary<string, string>
        {
            ["start"] = r.Start.ToString(),
            ["end"] = r.End.ToString(),
            ["granularity"] = "DAILY",
            ["metric_types"] = WhatsAppQuery.JsonArray(r.MetricTypes),
            ["group_ids"] = WhatsAppQuery.JsonArray(r.GroupIds)
        };
        return RunAsync("GetGroupAnalytics", () => Api.GetAsync<object>($"/{r.WabaId}/group_analytics", r.AccessToken, query), r);
    }

    // Flow endpoint metrics: GET /{flow-id}?fields=metric.name(..).granularity(..).since(..).until(..)
    public Task<Result<object>> GetFlowMetricsAsync(WaFlowMetricsRequest r)
    {
        var field = $"metric.name({r.MetricName}).granularity({r.Granularity})";
        if (!string.IsNullOrEmpty(r.Since)) field += $".since({r.Since})";
        if (!string.IsNullOrEmpty(r.Until)) field += $".until({r.Until})";
        return RunAsync("GetFlowMetrics", () => Api.GetAsync<object>($"/{r.FlowId}", r.AccessToken,
            new Dictionary<string, string> { ["fields"] = field }), r);
    }
}
