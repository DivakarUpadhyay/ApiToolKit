using ApiToolKit.Infrastructure;
using ApiToolKit.Infrastructure.Data;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "ApiToolKit - Google Analytics (GA4) API",
        Version = "v1",
        Description = "ASP.NET Core Clean Architecture integration for Google Analytics Data API v1beta (GA4). Covers OAuth 2.0 authentication, standard reports, batch reports, pivot reports, funnel reports, realtime reports, metadata, and property/account management via the Analytics Admin API.",
        Contact = new OpenApiContact { Name = "ApiToolKit", Email = "official@amplegroupglobal.com" }
    });

    c.SwaggerDoc("admin", new OpenApiInfo
    {
        Title = "ApiToolKit - Google Analytics Admin API",
        Version = "v1alpha",
        Description = "Google Analytics Admin API integration for managing accounts, properties, and data streams."
    });

    c.CustomSchemaIds(type => type.FullName);

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
        c.IncludeXmlComments(xmlPath);
});

builder.Services.AddInfrastructure();

var app = builder.Build();

// Creates/updates the database from Database*.sql (idempotent) before serving requests.
app.Services.InitializeDatabase("ApiToolKit_Analytics");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Google Analytics GA4 API v1");
        c.RoutePrefix = "swagger";
        c.DocumentTitle = "ApiToolKit - Google Analytics (GA4) API";
    });
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapControllers();

app.Run();
