using Aquifer.API.Clients.Http.Auth0;

namespace Aquifer.API.Services;

public static class AquiferHttpServices
{
    public static IServiceCollection AddAquiferHttpServices(this IServiceCollection services)
    {
        services.AddHttpClient<IAuth0HttpClient, Auth0HttpClient>();

        return services;
    }
}