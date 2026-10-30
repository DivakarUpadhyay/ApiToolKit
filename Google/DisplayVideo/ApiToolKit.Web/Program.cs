using ApiToolKit.Infrastructure;
using ApiToolKit.Infrastructure.Data;
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
        Title = "ApiToolKit - Display & Video 360 API",
        Version = "v1",
        Description = "ASP.NET Core Clean Architecture integration for Display &amp; Video 360 API v4: every resource - partners, advertisers, campaigns, insertion orders, line items, ad groups, creatives, audiences, inventory sources, deals, channels, targeting options, users and more. Connect a Google account with GET /api/displayvideo/auth/url?accountKey=..., then pass the same accountKey to every endpoint.",
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

// Creates/updates the database from Database\*.sql (idempotent) before serving requests.
app.Services.InitializeDatabase("ApiToolKit_DisplayVideo");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Display & Video 360 API v1");
        c.RoutePrefix = "swagger";
        c.DocumentTitle = "ApiToolKit - Display & Video 360 API";
    });
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapControllers();

app.Run();
