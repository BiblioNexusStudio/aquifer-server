using Aquifer.Well.API.Helpers;
using FastEndpoints.Swagger;
using NSwag;

namespace Aquifer.Well.API.OpenApi;

public static class SwaggerDocumentSettings
{
    public const string DocumentName = "v1";

    public static IServiceCollection AddSwaggerDocumentSettings(this IServiceCollection services)
    {
        return services
            .SwaggerDocument(sd =>
        {
            // turn off auto-grouping (but now must manually tag each endpoint using `WithTags()`)
            sd.AutoTagPathSegmentIndex = 0;

            sd.ShortSchemaNames = true;
            sd.EnableJWTBearerAuth = false;
            sd.EndpointFilter = ep => ep.EndpointTags?.Contains(EndpointHelpers.EndpointTags.ExcludeFromSwaggerDocument) != true;
            sd.DocumentSettings = ds =>
            {
                ds.DocumentName = DocumentName;
                ds.Title = "Aquifer Well API Documentation";
                ds.Description = "All endpoints require an API key in the `api-key` header.";
                ds.AddAuth(
                    "ApiKey",
                    new OpenApiSecurityScheme
                    {
                        Name = "api-key",
                        In = OpenApiSecurityApiKeyLocation.Header,
                        Type = OpenApiSecuritySchemeType.ApiKey
                    });
                ds.MarkNonNullablePropsAsRequired();
                ds.SchemaSettings.SchemaProcessors.Add(new EnumSchemaProcessor());
                ds.OperationProcessors.Add(new DefaultParameterOperationProcessor());
            };
            sd.TagDescriptions = td =>
            {
                td["Languages"] = "Endpoints for pulling data specific to languages.";
                td["Bibles"] = "Endpoints for discovering available Bibles and pulling down Bible text and audio information.";
            };
        });
    }
}