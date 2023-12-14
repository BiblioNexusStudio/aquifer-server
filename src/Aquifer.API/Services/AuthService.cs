﻿using Aquifer.API.Common;
using Aquifer.API.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

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

        // this is an example of how a policy can be added, where the permissions
        // are set in Auth0 for the given user
        services.AddAuthorization(options => options.AddPolicy(PermissionName.Write,
            p => p.RequireClaim("user").RequireClaim("permissions", "write:values")));

        services.AddAuthorization(options => options.AddPolicy(PermissionName.Read,
            p => p.RequireClaim("user").RequireClaim("permissions", "read:values")));

        services.AddAuthorization(options => options.AddPolicy(PermissionName.Aquiferize,
            p => p.RequireClaim("user").RequireClaim("permissions", "aquiferize:content")));

        services.AddAuthorization(options => options.AddPolicy(PermissionName.Publish,
            p => p.RequireClaim("user").RequireClaim("permissions", "publish:content")));

        return services;
    }

    public static void UseAuth(this WebApplication app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
    }
}