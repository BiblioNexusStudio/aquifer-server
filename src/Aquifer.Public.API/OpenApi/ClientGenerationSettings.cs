using FastEndpoints.ClientGen.Kiota;
using Kiota.Builder;

namespace Aquifer.Public.API.OpenApi;

/// <summary>
/// Generate client code and make it available as a zip file per language at `/clients/language-id`.
/// See https://fast-endpoints.com/docs/swagger-support#api-client-generation.
/// We could also generate clients from the command line or on build if desired.
/// </summary>
public static class ClientGenerationSettings
{
    public static IEndpointRouteBuilder ConfigureClientGeneration(
        this IEndpointRouteBuilder app,
        string swaggerDocumentName,
        TimeSpan clientZipCacheDuration)
    {
        app.MapApiClientEndpoint(
            "/clients/cs",
            c =>
            {
                c.SwaggerDocumentName = swaggerDocumentName;
                c.Language = GenerationLanguage.CSharp;
                c.ClientNamespaceName = "BiblioNexus.Aquifer.API.Client";
                c.ClientClassName = "AquiferClient";
                c.OutputPath = Path.Combine(c.OutputPath, "cs");
                c.CleanOutput = true;
            },
            o =>
            {
                o.CacheOutput(p => p.Expire(clientZipCacheDuration));
                o.ExcludeFromDescription();
            });

        app.MapApiClientEndpoint(
            "/clients/java",
            c =>
            {
                c.SwaggerDocumentName = swaggerDocumentName;
                c.Language = GenerationLanguage.Java;
                c.ClientNamespaceName = "org.biblionexus.aquifer.api.client";
                c.ClientClassName = "AquiferClient";
                c.OutputPath = Path.Combine(c.OutputPath, "java");
                c.CleanOutput = true;
            },
            o =>
            {
                o.CacheOutput(p => p.Expire(clientZipCacheDuration));
                o.ExcludeFromDescription();
            });

        app.MapApiClientEndpoint(
            "/clients/py",
            c =>
            {
                c.SwaggerDocumentName = swaggerDocumentName;
                c.Language = GenerationLanguage.Python;
                c.ClientNamespaceName = "biblionexus_aquifer_api_client";
                c.ClientClassName = "AquiferClient";
                c.OutputPath = Path.Combine(c.OutputPath, "py");
                c.CleanOutput = true;
            },
            o =>
            {
                o.CacheOutput(p => p.Expire(clientZipCacheDuration));
                o.ExcludeFromDescription();
            });

        app.MapApiClientEndpoint(
            "/clients/ts",
            c =>
            {
                c.SwaggerDocumentName = swaggerDocumentName;
                c.Language = GenerationLanguage.TypeScript;
                c.ClientNamespaceName = "biblionexus-aquifer-api-client";
                c.ClientClassName = "AquiferClient";
                c.OutputPath = Path.Combine(c.OutputPath, "ts");
                c.CleanOutput = true;
            },
            o =>
            {
                o.CacheOutput(p => p.Expire(clientZipCacheDuration));
                o.ExcludeFromDescription();
            });

        return app;
    }
}