using FastEndpoints.ClientGen.Kiota;
using Kiota.Builder;

namespace Aquifer.Public.API.OpenApi;

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
                c.ClientNamespaceName = "BiblioNexus.Aquifer";
                c.ClientClassName = "AquiferClient";
                c.OutputPath = Path.Combine(c.OutputPath, "cs");
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
                c.ClientNamespaceName = "org.biblionexus.aquifer";
                c.ClientClassName = "AquiferClient";
                c.OutputPath = Path.Combine(c.OutputPath, "java");
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
                c.ClientNamespaceName = "biblionexus_aquifer";
                c.ClientClassName = "AquiferClient";
                c.OutputPath = Path.Combine(c.OutputPath, "py");
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
                c.ClientNamespaceName = "biblionexus-aquifer";
                c.ClientClassName = "AquiferClient";
                c.OutputPath = Path.Combine(c.OutputPath, "ts");
            },
            o =>
            {
                o.CacheOutput(p => p.Expire(clientZipCacheDuration));
                o.ExcludeFromDescription();
            });

        return app;
    }
}