using ApiToolKit.Application.Interfaces.Services.Identity;
using ApiToolKit.Application.Services.Identity;
using ApiToolKit.Domain.Interfaces.Repositories.Identity;
using ApiToolKit.Infrastructure.Data;
using ApiToolKit.Infrastructure.External.Identity;
using ApiToolKit.Infrastructure.Repositories.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace ApiToolKit.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<DapperContext>();

        // Repositories
        services.AddScoped<IIdentityAuthRepository, IdentityAuthRepository>();
        services.AddScoped<IContactRepository, ContactRepository>();
        services.AddScoped<IContactGroupRepository, ContactGroupRepository>();
        services.AddScoped<IIdentityUserRepository, IdentityUserRepository>();

        // Identity HTTP client
        services.AddHttpClient<IdentityApiClient>();
        services.AddScoped<IIdentityApiClient>(sp => sp.GetRequiredService<IdentityApiClient>());

        // Application Services
        services.AddScoped<IIdentityOAuthService, IdentityOAuthService>();
        services.AddScoped<IPeopleService, PeopleService>();
        services.AddScoped<IIdentityToolkitService, IdentityToolkitService>();
        services.AddScoped<IAdminUserService, AdminUserService>();

        return services;
    }
}
