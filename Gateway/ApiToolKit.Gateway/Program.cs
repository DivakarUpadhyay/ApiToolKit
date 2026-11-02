using ApiToolKit.Gateway;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<GatewayOptions>(builder.Configuration.GetSection("Gateway"));
builder.Services.AddHttpClient("spec", c => c.Timeout = TimeSpan.FromSeconds(3));
builder.Services.AddHttpClient("proxy", c => c.Timeout = TimeSpan.FromMinutes(5))
    .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler { AllowAutoRedirect = false, UseCookies = false });
builder.Services.AddSingleton<SpecCatalog>();
builder.Services.AddSingleton<ModuleProxy>();

var app = builder.Build();
var catalog = app.Services.GetRequiredService<SpecCatalog>();

// Learn every module's routes up front (live or cached) so "Try it out" can be forwarded immediately.
_ = catalog.WarmUpAsync();

app.MapGet("/", () => Results.Redirect("/swagger"));

// One OpenAPI document per platform group (Facebook, WhatsApp, Google, YouTube, TikTok …).
app.MapGet("/specs/{group}.json", async (string group, CancellationToken ct) =>
    await catalog.BuildGroupAsync(group, ct) is { } doc
        ? Results.Content(doc.ToJsonString(), "application/json")
        : Results.NotFound(new { success = false, message = $"Unknown group '{group}'." }));

// Module status: which ApiToolKit apps are running right now.
app.MapGet("/gateway/modules", async (CancellationToken ct) =>
{
    var rows = await Task.WhenAll(catalog.Modules.Select(async m => new
    {
        m.Key,
        m.Name,
        m.BaseUrl,
        m.Project,
        Running = await catalog.IsRunningAsync(m, ct),
        Groups = catalog.Groups.Where(g => g.Sources.Any(s => s.Module == m.Key)).Select(g => g.Title)
    }));
    return Results.Ok(rows);
});

app.UseSwaggerUI(c =>
{
    c.DocumentTitle = "ApiToolKit — API Groups";
    foreach (var group in catalog.Groups)
        c.SwaggerEndpoint($"/specs/{group.Key}.json", group.Title);
    c.DisplayRequestDuration();
    c.EnableFilter();
    c.EnableTryItOutByDefault();
    c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
    c.ConfigObject.AdditionalItems["urls.primaryName"] = catalog.Groups.FirstOrDefault()?.Title ?? string.Empty;
});

// Any other request is forwarded to the module that owns the path.
app.MapFallback((HttpContext ctx) => app.Services.GetRequiredService<ModuleProxy>().HandleAsync(ctx));

app.Run();
