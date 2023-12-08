using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using System.Runtime.Serialization;

namespace Aquifer.API.Services;

public static class SwaggerService
{
    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(x =>
        {
            x.AddSecurityDefinition("Bearer",
                new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the bearer scheme",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

            x.AddSecurityDefinition("ApiKey",
                new OpenApiSecurityScheme
                {
                    Description = "API Key header",
                    Name = "api-key",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

            x.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new List<string>()
                },
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "ApiKey"
                        }
                    },
                    new List<string>()
                }
            });

            // Hide all endpoints beginning with `/admin`
            x.DocInclusionPredicate((docName, apiDesc) =>
            {
                var routeTemplate = apiDesc.RelativePath;
                if (routeTemplate?.StartsWith("admin") == true)
                    return false;
                return true;
            });

            // Remove `Dto` from the end of the schema names
            x.CustomSchemaIds(type => type.Name.EndsWith("Dto") ? type.Name.Replace("Dto", string.Empty) : type.Name);

            // Show the string value for all enums
            x.SchemaFilter<EnumSchemaFilter>();
        });

        return services;
    }

    public static void UseSwaggerWithUi(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    private class EnumSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type.IsEnum)
            {
                schema.Enum.Clear();
                var enumType = context.Type;
                foreach (var name in Enum.GetNames(enumType))
                {
                    var member = enumType.GetMember(name).First();
                    var enumMemberAttribute = member.GetCustomAttribute<EnumMemberAttribute>();
                    var value = enumMemberAttribute?.Value ?? name;
                    schema.Enum.Add(new OpenApiString($"{value}"));
                }
            }
        }
    }
}