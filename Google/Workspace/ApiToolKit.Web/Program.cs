using ApiToolKit.Infrastructure;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "ApiToolKit - Google Workspace APIs",
        Version = "v1",
        Description = "ASP.NET Core Clean Architecture integration for all Google Workspace APIs. Covers Auth (OAuth 2.0), Gmail (Messages, Labels, Drafts, Threads, Settings), Google Drive (Files, Permissions, Revisions, Shared Drives), Google Calendar (Calendars, Events, ACL, Free/Busy), Google Sheets (Spreadsheets, Values, Batch Operations), Google Docs (Documents, Content Editing), Google Slides (Presentations, Slides, Elements), Google Meet (Spaces, Recordings, Participants, Conference Records), Google Chat (Spaces, Messages, Members), and Google Forms (Forms, Items, Responses) modules.",
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
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Google Workspace API v1");
        c.RoutePrefix = "swagger";
        c.DocumentTitle = "ApiToolKit - Google Workspace APIs";
    });
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapControllers();

app.Run();
