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
        Title = "ApiToolKit - Google Tag Manager API",
        Version = "v1",
        Description = "ASP.NET Core Clean Architecture integration for Tag Manager API v2: accounts, containers, workspaces, tags, triggers, variables, versions and publishing. Connect a Google account with GET /api/tagmanager/auth/url?accountKey=..., then pass the same accountKey to every endpoint.",
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
app.Services.InitializeDatabase("ApiToolKit_TagManager");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Google Tag Manager API v1");
        c.RoutePrefix = "swagger";
        c.DocumentTitle = "ApiToolKit - Google Tag Manager API";
    });
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapControllers();

app.Run();
