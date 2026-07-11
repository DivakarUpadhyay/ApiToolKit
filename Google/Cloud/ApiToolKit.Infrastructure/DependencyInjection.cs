using ApiToolKit.Application.Interfaces.Services.Cloud;
using ApiToolKit.Application.Services.Cloud;
using ApiToolKit.Domain.Interfaces.Repositories.Cloud;
using ApiToolKit.Infrastructure.Data;
using ApiToolKit.Infrastructure.External.Cloud;
using ApiToolKit.Infrastructure.Repositories.Cloud;
using Microsoft.Extensions.DependencyInjection;

namespace ApiToolKit.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<DapperContext>();

        // Repositories
        services.AddScoped<ICloudAuthRepository, CloudAuthRepository>();
        services.AddScoped<IVisionRepository, VisionRepository>();
        services.AddScoped<ISpeechRepository, SpeechRepository>();
        services.AddScoped<ILanguageRepository, LanguageRepository>();
        services.AddScoped<ITranslationRepository, TranslationRepository>();

        // HTTP Client
        services.AddHttpClient<CloudApiClient>();
        services.AddScoped<ICloudApiClient>(sp => sp.GetRequiredService<CloudApiClient>());

        // Application Services
        services.AddScoped<ICloudAuthService, CloudAuthService>();
        services.AddScoped<IVisionService, VisionService>();
        services.AddScoped<ISpeechService, SpeechService>();
        services.AddScoped<ILanguageService, LanguageService>();
        services.AddScoped<ITranslationService, TranslationService>();

        return services;
    }
}
