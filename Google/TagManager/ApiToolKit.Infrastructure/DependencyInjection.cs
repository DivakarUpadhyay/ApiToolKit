using ApiToolKit.Application.Interfaces.Services.TagManager;
using ApiToolKit.Application.Services.TagManager;
using ApiToolKit.Domain.Interfaces.Repositories.TagManager;
using ApiToolKit.Infrastructure.Data;
using ApiToolKit.Infrastructure.External.TagManager;
using ApiToolKit.Infrastructure.Repositories.TagManager;
using Microsoft.Extensions.DependencyInjection;

namespace ApiToolKit.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<DapperContext>();

        // Repositories
        services.AddScoped<ITagManagerAuthRepository, TagManagerAuthRepository>();

        // Google Tag Manager API HTTP client
        services.AddHttpClient<TagManagerApiClient>();
        services.AddScoped<ITagManagerApiClient>(sp => sp.GetRequiredService<TagManagerApiClient>());

        // Application Services
        services.AddScoped<ITagManagerAuthService, TagManagerAuthService>();
        services.AddScoped<ITagManagerAccountService, TagManagerAccountService>();
        services.AddScoped<ITagManagerContainerService, TagManagerContainerService>();
        services.AddScoped<ITagManagerWorkspaceService, TagManagerWorkspaceService>();
        services.AddScoped<ITagManagerTagService, TagManagerTagService>();
        services.AddScoped<ITagManagerTriggerService, TagManagerTriggerService>();
        services.AddScoped<ITagManagerVariableService, TagManagerVariableService>();
        services.AddScoped<ITagManagerVersionService, TagManagerVersionService>();
        services.AddScoped<ITagManagerFolderService, TagManagerFolderService>();
        services.AddScoped<ITagManagerTemplateService, TagManagerTemplateService>();
        services.AddScoped<ITagManagerClientService, TagManagerClientService>();
        services.AddScoped<ITagManagerTransformationService, TagManagerTransformationService>();
        services.AddScoped<ITagManagerZoneService, TagManagerZoneService>();
        services.AddScoped<ITagManagerGtagConfigService, TagManagerGtagConfigService>();
        services.AddScoped<ITagManagerBuiltInVariableService, TagManagerBuiltInVariableService>();
        services.AddScoped<ITagManagerEnvironmentService, TagManagerEnvironmentService>();
        services.AddScoped<ITagManagerDestinationService, TagManagerDestinationService>();
        services.AddScoped<ITagManagerUserPermissionService, TagManagerUserPermissionService>();

        return services;
    }
}
