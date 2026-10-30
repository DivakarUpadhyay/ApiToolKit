using ApiToolKit.Application.Interfaces.Services.BusinessProfile;
using ApiToolKit.Application.Services.BusinessProfile;
using ApiToolKit.Domain.Interfaces.Repositories.BusinessProfile;
using ApiToolKit.Infrastructure.Data;
using ApiToolKit.Infrastructure.External.BusinessProfile;
using ApiToolKit.Infrastructure.Repositories.BusinessProfile;
using Microsoft.Extensions.DependencyInjection;

namespace ApiToolKit.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<DapperContext>();

        // Repositories
        services.AddScoped<IBusinessProfileAuthRepository, BusinessProfileAuthRepository>();

        // Google Business Profile APIs HTTP client
        services.AddHttpClient<BusinessProfileApiClient>();
        services.AddScoped<IBusinessProfileApiClient>(sp => sp.GetRequiredService<BusinessProfileApiClient>());

        // Application Services
        services.AddScoped<IBusinessProfileAuthService, BusinessProfileAuthService>();
        services.AddScoped<IBusinessProfileAccountService, BusinessProfileAccountService>();
        services.AddScoped<IBusinessProfileLocationService, BusinessProfileLocationService>();
        services.AddScoped<IBusinessProfileCategoryService, BusinessProfileCategoryService>();
        services.AddScoped<IBusinessProfileReviewService, BusinessProfileReviewService>();
        services.AddScoped<IBusinessProfileLocalPostService, BusinessProfileLocalPostService>();
        services.AddScoped<IBusinessProfilePerformanceService, BusinessProfilePerformanceService>();
        services.AddScoped<IBusinessProfileMediaService, BusinessProfileMediaService>();
        services.AddScoped<IBusinessProfileVerificationService, BusinessProfileVerificationService>();
        services.AddScoped<IBusinessProfilePlaceActionService, BusinessProfilePlaceActionService>();
        services.AddScoped<IBusinessProfileLodgingService, BusinessProfileLodgingService>();

        return services;
    }
}
