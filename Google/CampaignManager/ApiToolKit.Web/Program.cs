using ApiToolKit.Infrastructure;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Newtonsoft keeps Google's JSON responses (JToken) intact in API results.
builder.Services.AddControllersWithViews().AddNewtonsoftJson();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "ApiToolKit - Campaign Manager 360 API",
        Version = "v1",
        Description = "ASP.NET Core Clean Architecture integration for Campaign Manager 360 API v5 (dfareporting): every resource - user profiles, accounts, advertisers, campaigns, placements, sites, ads, creatives, floodlight, conversions, targeting, reports and report files. Connect a Google account with GET /api/campaignmanager/auth/url?accountKey=..., then pass the same accountKey to every endpoint.",
        Contact = new OpenApiContact { Name = "ApiToolKit", Email = "official@amplegroupglobal.com" }
    });

    c.CustomSchemaIds(type => type.FullName);

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
        c.IncludeXmlComments(xmlPath);
});

builder.Services.AddInfrastructure();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Campaign Manager 360 API v1");
        c.RoutePrefix = "swagger";
        c.DocumentTitle = "ApiToolKit - Campaign Manager 360 API";
    });
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapControllers();

app.Run();
