using FastEndpoints.Swagger;
using NSwag;

namespace Aquifer.Public.API.OpenApi;

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
                ds.Description = "All endpoints require an API key in the 'api-key' header.";
                ds.AddAuth("ApiKey",
                    new OpenApiSecurityScheme
                    {
                        Name = "api-key",
                        In = OpenApiSecurityApiKeyLocation.Header,
                        Type = OpenApiSecuritySchemeType.ApiKey
                    });
                ds.SchemaSettings.SchemaProcessors.Add(new EnumSchemaProcessor());
                ds.OperationProcessors.Add(new DefaultParameterOperationProcessor());
            };
            sd.TagDescriptions = td =>
            {
                td["Resources"] = """
                                  These endpoints allow searching for resources and pulling down the associated content for each one.
                                  An individual resource is the individual content of a resource, such as a specific study note within
                                  a group of associated study notes. A group of resources is a collection of individual resources belonging
                                  to the same collection. A resource type is the category to which a group of resources can belong.

                                  As an example, for the Tyndale Bible Dictionary article for "Aaronn", the resource is "Aaronn", the
                                  title of the group/collection to which it belongs is "Bible Dictionary (Tyndale)", and the resource type
                                  is "Dictionary".
                                  """;
                td["Languages"] = "Endpoints for pulling data specific to languages.";
                td["Bible-Books"] = "Endpoints for pulling data specific to Bible books.";
            };
        });
    }
}