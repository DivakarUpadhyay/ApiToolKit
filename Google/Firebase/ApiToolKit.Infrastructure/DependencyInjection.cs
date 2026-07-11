using ApiToolKit.Application.Interfaces.Services.Firebase;
using ApiToolKit.Application.Services.Firebase;
using ApiToolKit.Domain.Interfaces.Repositories.Firebase;
using ApiToolKit.Infrastructure.Data;
using ApiToolKit.Infrastructure.External.Firebase;
using ApiToolKit.Infrastructure.Repositories.Firebase;
using Microsoft.Extensions.DependencyInjection;

namespace ApiToolKit.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<DapperContext>();

        // Repositories
        services.AddScoped<IFirebaseAuthRepository, FirebaseAuthRepository>();
        services.AddScoped<IFcmRepository, FcmRepository>();
        services.AddScoped<IFirebaseUserRepository, FirebaseUserRepository>();
        services.AddScoped<IRemoteConfigRepository, RemoteConfigRepository>();

        // Firebase HTTP client
        services.AddHttpClient<FirebaseApiClient>();
        services.AddScoped<IFirebaseApiClient>(sp => sp.GetRequiredService<FirebaseApiClient>());

        // Application Services
        services.AddScoped<IFirebaseAuthService2, FirebaseAuthService2>();
        services.AddScoped<IFcmService, FcmService>();
        services.AddScoped<IFirebaseUserService, FirebaseUserService>();
        services.AddScoped<IRemoteConfigService, RemoteConfigService>();
        services.AddScoped<IAppCheckService, AppCheckService>();

        return services;
    }
}
