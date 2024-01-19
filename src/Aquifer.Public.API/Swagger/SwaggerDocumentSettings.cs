using FastEndpoints.Swagger;
using NSwag;

namespace Aquifer.Public.API.Swagger;

public static class SwaggerDocumentSettings
{
    public static IServiceCollection AddSwaggerDocumentSettings(this IServiceCollection service)
    {
        return service.SwaggerDocument(sd =>
        {
            sd.ShortSchemaNames = true;
            sd.EnableJWTBearerAuth = false;
            sd.DocumentSettings = ds =>
            {
                ds.Title = "Aquifer API";
                ds.AddAuth("ApiKey",
                    new OpenApiSecurityScheme
                    {
                        Name = "api-key",
                        In = OpenApiSecurityApiKeyLocation.Header,
                        Type = OpenApiSecuritySchemeType.ApiKey
                    });
                ds.SchemaSettings.SchemaProcessors.Add(new EnumSchemaProcessor());
                ds.OperationProcessors.Add(new MakeDefaultingParametersOptionalProcessor());
            };
        });
    }
}

