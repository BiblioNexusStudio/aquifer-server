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
    private const string _clientsRouteName = "clients";

    public static IEndpointRouteBuilder ConfigureClientGeneration(
        this IEndpointRouteBuilder app,
        string swaggerDocumentName,
        TimeSpan clientZipCacheDuration)
    {
        app.MapApiClientEndpoint(
            $"/{_clientsRouteName}/cs",
            c =>
            {
                c.SwaggerDocumentName = swaggerDocumentName;
                c.Language = GenerationLanguage.CSharp;
                c.ClientNamespaceName = "BiblioNexus.Aquifer.API.Client";
                c.ClientClassName = "AquiferClient";
                c.OutputPath = Path.Combine(c.OutputPath, "cs", Path.GetRandomFileName());
                c.ExcludePatterns = [$"**/{_clientsRouteName}/**"];
            },
            o =>
            {
                o.CacheOutput(p => p.Expire(clientZipCacheDuration));
                o.WithTags("Clients");
                o.WithSummary("Downloads C# client.");
                o.WithDescription("""
                    Downloads a zip file containing a generated C# client to use when calling this API.
                    The generated source code uses [Kiota](https://learn.microsoft.com/en-us/openapi/kiota/) in order to make web requests. You will need to install the following dependencies:
                      * `dotnet add package Microsoft.Kiota.Bundle --version 1.15.2` (or newer version)

                    See also [this C# Kiota example](https://github.com/microsoft/kiota-samples/blob/main/get-started/quickstart/dotnet/src/Program.cs) for how to use a generated Kiota client.
                    """);
                o.WithOpenApi();
            });

        app.MapApiClientEndpoint(
            $"/{_clientsRouteName}/java",
            c =>
            {
                c.SwaggerDocumentName = swaggerDocumentName;
                c.Language = GenerationLanguage.Java;
                c.ClientNamespaceName = "org.biblionexus.aquifer.api.client";
                c.ClientClassName = "AquiferClient";
                c.OutputPath = Path.Combine(c.OutputPath, "java", Path.GetRandomFileName());
                c.ExcludePatterns = [$"**/{_clientsRouteName}/**"];
            },
            o =>
            {
                o.CacheOutput(p => p.Expire(clientZipCacheDuration));
                o.WithTags("Clients");
                o.WithSummary("Download Java client.");
                o.WithDescription("""
                    Downloads a zip file containing a generated Java client to use when calling this API.
                    The generated source code uses [Kiota](https://learn.microsoft.com/en-us/openapi/kiota/) in order to make web requests. You will need to install the following dependencies:
                      * `com.microsoft.kiota:microsoft-kiota-bundle:1.8.0` (or newer version)
                      * `jakarta.annotation:jakarta.annotation-api:2.1.1` (or newer version)

                    See also [this Java Kiota example](https://github.com/microsoft/kiota-samples/blob/main/get-started/quickstart/java/app/src/main/java/kiotaposts/App.java) for how to use a generated Kiota client.
                    """);
                o.WithOpenApi();
            });

        app.MapApiClientEndpoint(
            $"/{_clientsRouteName}/py",
            c =>
            {
                c.SwaggerDocumentName = swaggerDocumentName;
                c.Language = GenerationLanguage.Python;
                c.ClientNamespaceName = "biblionexus_aquifer_api_client";
                c.ClientClassName = "AquiferClient";
                c.OutputPath = Path.Combine(c.OutputPath, "py", Path.GetRandomFileName());
                c.ExcludePatterns = [$"**/{_clientsRouteName}/**"];
            },
            o =>
            {
                o.CacheOutput(p => p.Expire(clientZipCacheDuration));
                o.WithTags("Clients");
                o.WithSummary("Download Python client.");
                o.WithDescription("""
                    Downloads a zip file containing a generated Python client to use when calling this API.
                    The generated source code uses [Kiota](https://learn.microsoft.com/en-us/openapi/kiota/) in order to make web requests. You will need to install the following dependencies:
                      * `pip install microsoft-kiota-bundle==1.6.6` (or newer version)

                    See also [this Python Kiota example](https://github.com/microsoft/kiota-samples/blob/main/get-started/quickstart/python/main.py) for how to use a generated Kiota client.
                    """);
                o.WithOpenApi();
            });

        app.MapApiClientEndpoint(
            $"/{_clientsRouteName}/ts",
            c =>
            {
                c.SwaggerDocumentName = swaggerDocumentName;
                c.Language = GenerationLanguage.TypeScript;
                c.ClientNamespaceName = "biblionexus-aquifer-api-client";
                c.ClientClassName = "AquiferClient";
                c.OutputPath = Path.Combine(c.OutputPath, "ts", Path.GetRandomFileName());
                c.ExcludePatterns = [$"**/{_clientsRouteName}/**"];
            },
            o =>
            {
                o.CacheOutput(p => p.Expire(clientZipCacheDuration));
                o.WithTags("Clients");
                o.WithSummary("Download TypeScript client.");
                o.WithDescription("""
                    Downloads a zip file containing a generated TypeScript client to use when calling this API.
                    The generated source code uses [Kiota](https://learn.microsoft.com/en-us/openapi/kiota/) in order to make web requests. You will need to install the following dependencies:
                      * `npm install @microsoft/kiota-bundle@1.0.0-preview.77 -SE` (or newer version)

                    See also [this TypeScript Kiota example](https://github.com/microsoft/kiota-samples/blob/main/get-started/quickstart/typescript/index.ts) for how to use a generated Kiota client.
                    """);
                o.WithOpenApi();
            });

        return app;
    }
}