﻿using System.Security.Claims;
using Aquifer.API.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Aquifer.API.Services;

public static class AuthService
{
    public static IServiceCollection AddAuth(this IServiceCollection services, JwtSettingOptions? jwtSettings)
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

        return services;
    }

    public static void UseAuth(this WebApplication app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
    }
}