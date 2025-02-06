using System.Security.Claims;
using Aquifer.API.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Aquifer.API.Services;

public static class AuthServiceRegistry
{
    public static IServiceCollection AddAuthServices(this IServiceCollection services, JwtSettingOptions? jwtSettings)
    {
        ArgumentNullException.ThrowIfNull(jwtSettings);

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = jwtSettings.Authority;
                options.Audience = jwtSettings.Audience;
                options.TokenValidationParameters =
                    new TokenValidationParameters { NameClaimType = ClaimTypes.NameIdentifier };
            });

        services.AddAuthorization();

        services.AddSingleton(cfg => cfg.GetService<IOptions<ConfigurationOptions>>()!.Value.Auth0Settings);
        services.AddScoped<IAuth0Service, Auth0Service>();

        return services;
    }

    public static void UseAuth(this WebApplication app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
    }
}