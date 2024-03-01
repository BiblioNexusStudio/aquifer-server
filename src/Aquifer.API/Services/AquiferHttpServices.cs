using Aquifer.API.Clients.Http.Auth0;
using Aquifer.API.Clients.Http.OpenAI;

namespace Aquifer.API.Services;

public static class AquiferHttpServices
{
    public static IServiceCollection AddAquiferHttpServices(this IServiceCollection services)
    {
        services.AddHttpClient<IAuth0HttpClient, Auth0HttpClient>();
        services.AddHttpClient<IOpenAiHttpClient, OpenAiHttpClient>();

        return services;
    }
}