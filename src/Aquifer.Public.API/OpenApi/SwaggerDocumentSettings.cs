using Aquifer.Public.API.Helpers;
using FastEndpoints.Swagger;
using NSwag;

namespace Aquifer.Public.API.OpenApi;

public static class SwaggerDocumentSettings
{
    public static IServiceCollection AddSwaggerDocumentSettings(this IServiceCollection service)
    {
        return service.SwaggerDocument(sd =>
        {
            // turn off auto-grouping (but now must manually tag each endpoint using `WithTags()`)
            sd.AutoTagPathSegmentIndex = 0;

            sd.ShortSchemaNames = true;
            sd.EnableJWTBearerAuth = false;
            sd.EndpointFilter = ep => ep.EndpointTags?.Contains(EndpointHelpers.EndpointTags.ExcludeFromSwaggerDocument) != true;
            sd.DocumentSettings = ds =>
            {
                ds.Title = "Aquifer API";
                ds.Description = """
                                 All endpoints require an API key in the `api-key` header.<br><br>

                                 The following videos are available for anyone new to working with APIs:

                                 <a href="https://cdn.aquifer.bible/training/aquifer-api-documentation.mp4" target="_blank">Understanding the documentation</a>

                                 <a href="https://cdn.aquifer.bible/training/aquifer-api-postman.mp4" target="_blank">Making requests with Postman</a>
                                 """;
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
                                  a collection of associated study notes. A collection of resources is a group of individual resources belonging
                                  to the same collection. A resource type is the category to which a collection of resources can belong.

                                  As an example, for the Tyndale Bible Dictionary article for "Aaron", the resource is "Aaron", the
                                  title of the collection to which it belongs is "Bible Dictionary (Tyndale)", and the resource type
                                  is "Dictionary".
                                  """;
                td["Resources/Collections"] = "Endpoints for retrieving collections of resources.";
                td["Resources/Types"] = "Endpoints for retrieving the different types of resource collections and resources.";
                td["Languages"] = "Endpoints for pulling data specific to languages.";
                td["Bibles"] = "Endpoints for discovering available Bibles and pulling down Bible text and audio information.";
            };
        });
    }
}