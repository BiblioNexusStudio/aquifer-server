using Aquifer.Common.Services.Caching;

namespace Aquifer.Public.API.Services;

public static class ServiceRegistry
{
    public static IServiceCollection AddCachingServices(this IServiceCollection services)
    {
        services.AddScoped<ICachingLanguageService, CachingLanguageService>();
        services.AddScoped<ICachingApiKeyService, CachingApiKeyService>();

        return services;
    }
}