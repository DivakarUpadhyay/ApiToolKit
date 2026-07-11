using ApiToolKit.Infrastructure;
using ApiToolKit.Web.Middleware;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(o => o.AddPolicy("AllowAll", p =>
    p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

builder.Services.AddControllersWithViews().AddNewtonsoftJson();
builder.Services.AddEndpointsApiExplorer();

// Sequential tag map: controller name → numbered display tag (01–31)
var tagMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
{
    { "FacebookAuth",             "01. Auth" },
    { "FacebookUser",             "02. User" },
    { "FacebookBusinessManager",  "03. Business Manager" },
    { "FacebookPage",             "04. Page" },
    { "FacebookPost",             "05. Post" },
    { "FacebookComment",          "06. Comment" },
    { "FacebookPhoto",            "07. Photo" },
    { "FacebookVideo",            "08. Video" },
    { "FacebookEvent",            "09. Event" },
    { "FacebookGroup",            "10. Group" },
    { "FacebookReaction",         "11. Reaction" },
    { "FacebookInsight",          "12. Insight" },
    { "FacebookAdAccount",        "13. Ad Account" },
    { "FacebookCampaign",         "14. Campaign" },
    { "FacebookAdSet",            "15. Ad Set" },
    { "FacebookAd",               "16. Ad" },
    { "FacebookPixel",            "17. Pixel" },
    { "FacebookCapi",             "18. Conversions API" },
    { "FacebookCatalog",          "19. Catalog" },
    { "FacebookInstagram",        "20. Instagram" },
    { "FacebookInstagramShopping","21. Instagram Shopping" },
    { "FacebookLive",             "22. Live Video" },
    { "FacebookMessenger",        "23. Messenger" },
    { "FacebookThreads",          "24. Threads" },
    { "FacebookWebhook",          "25. Webhook" },
    { "FacebookCommerce",         "26. Commerce" },
    { "FacebookProductFeed",      "27. Product Feed" },
    { "FacebookWhatsApp",         "28. WhatsApp" },
    { "FacebookWhatsAppCatalog",  "29. WhatsApp Catalog" },
    { "FacebookLeadGen",          "30. Lead Generation" },
    { "FacebookWhatsAppFlows",    "31. WhatsApp Flows" },
};

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "ApiToolKit — Facebook Graph API",
        Version = "v1",
        Description = "Complete Meta platform integration: Facebook · Instagram · WhatsApp · Threads — 31 modules · 347 endpoints.",
        Contact = new OpenApiContact { Name = "ApiToolKit", Email = "official@amplegroupglobal.com" }
    });

    // Assign numbered tags based on controller name
    c.TagActionsBy(api =>
    {
        var controller = api.ActionDescriptor.RouteValues["controller"] ?? string.Empty;
        return tagMap.TryGetValue(controller, out var tag)
            ? new[] { tag }
            : new[] { controller };
    });

    // Sort actions within each tag by HTTP method then route
    c.OrderActionsBy(api =>
    {
        var controller = api.ActionDescriptor.RouteValues["controller"] ?? string.Empty;
        tagMap.TryGetValue(controller, out var tag);
        return $"{tag}_{api.HttpMethod}_{api.RelativePath}";
    });

    c.CustomSchemaIds(type => type.FullName);
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
        c.IncludeXmlComments(xmlPath);
});

builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Facebook API v1");
        c.RoutePrefix = "swagger";
        c.DocumentTitle = "ApiToolKit — Facebook APIs";
        // Sort tags alphabetically so "01." comes before "02." ... "31."
        c.ConfigObject.AdditionalItems["tagsSorter"] = "alpha";
        c.ConfigObject.AdditionalItems["operationsSorter"] = "alpha";
        c.DefaultModelsExpandDepth(-1); // hide schemas section by default
        c.DisplayRequestDuration();
    });
}

app.UseCors("AllowAll");
app.UseMiddleware<ApiExceptionMiddleware>();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapControllers();

app.Run();
