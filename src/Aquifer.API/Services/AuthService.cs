﻿using Aquifer.API.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace Aquifer.API.Services;

public static class AuthService
{
    public static IServiceCollection AddAuth(this IServiceCollection services, ConfigurationOptions configuration)
    {
        ArgumentNullException.ThrowIfNull(configuration?.JwtSettings);

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = configuration.JwtSettings.Authority;
                options.Audience = configuration.JwtSettings.Audience;
                options.TokenValidationParameters =
                    new TokenValidationParameters { NameClaimType = ClaimTypes.NameIdentifier };
            });

        // this is an example of how a policy can be added, where the permissions
        // are set in Auth0 for the given user
        services.AddAuthorization(options => options.AddPolicy("write",
            p => p.RequireClaim("permissions", "write:values")));

        return services;
    }

    public static void UseAuth(this WebApplication app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
    }
}