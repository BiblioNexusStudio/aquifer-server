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
    public const string ClientGenerationOutputCacheTag = "client-generation";

    private const string ClientsRouteName = "clients";

    public static IEndpointRouteBuilder ConfigureClientGeneration(
        this IEndpointRouteBuilder app,
        string swaggerDocumentName,
        TimeSpan clientZipCacheDuration)
    {
        app.MapApiClientEndpoint(
            $"/{ClientsRouteName}/cs",
            c =>
            {
                c.SwaggerDocumentName = swaggerDocumentName;
                c.Language = GenerationLanguage.CSharp;
                c.ClientNamespaceName = "BiblioNexus.Aquifer.API.Client";
                c.ClientClassName = "AquiferClient";
                c.OutputPath = Path.Combine(c.OutputPath, "cs", Path.GetRandomFileName());
                c.ExcludePatterns = [$"**/{ClientsRouteName}/**", "**/admin/**"];
            },
            o =>
            {
                o.CacheOutput(p => p.Expire(clientZipCacheDuration).Tag(ClientGenerationOutputCacheTag));
                o.WithTags("Clients");
                o.WithSummary("Download C# client.");
                o.WithDescription(""""
                    Downloads a zip file containing a generated C# client to use when calling this API.
                    The generated source code uses [Kiota](https://learn.microsoft.com/en-us/openapi/kiota/) in order to make web requests. You will need to install the following dependencies:
                      * `dotnet add package Microsoft.Kiota.Bundle --version 1.15.2` (or newer version)
                    
                    The following is a full example of a `Program.cs` file using the generated client and injecting an API key header.
                    The example code assumes that the downloaded client code files are contained in the same solution in the `BiblioNexus.Aquifer.API.Client` namespace.
                    ```
                    using System.Net.Http;
                    using BiblioNexus.Aquifer.API.Client;
                    using Microsoft.Kiota.Abstractions.Authentication;
                    using Microsoft.Kiota.Http.HttpClientLibrary;
                    
                    // see https://learn.microsoft.com/en-us/openapi/kiota/middleware?tabs=csharp
                    var httpMessageHandler = KiotaClientFactory.ChainHandlersCollectionAndGetFirstLink(
                        KiotaClientFactory.GetDefaultHttpMessageHandler(),
                        [..KiotaClientFactory.CreateDefaultHandlers(), new SetApiKeyHeaderRequestHandler("your-api-key-goes-here")]);
                    var adapter = new HttpClientRequestAdapter(
                        new AnonymousAuthenticationProvider(),
                        httpClient: new HttpClient(httpMessageHandler!))
                    {
                        BaseUrl = "https://api.aquifer.bible",
                    };
                    
                    var aquiferClient = new AquiferClient(adapter);
                    
                    try
                    {
                        // GET /bibles/books
                        var bibleBooks = await aquiferClient.Bibles.Books.GetAsync();
                        
                        Console.WriteLine($"Retrieved {bibleBooks?.Count} Bible books.");
                        
                        // GET /bibles
                        var englishDefaultBible = (await aquiferClient.Bibles.GetAsync(b =>
                        {
                            b.QueryParameters.LanguageCode = "eng";
                            b.QueryParameters.IsLanguageDefault = true;
                        }))
                        ?.SingleOrDefault();
                        
                        Console.WriteLine($"The English language default Bible is \"{englishDefaultBible?.Name}\" ({englishDefaultBible?.Abbreviation}).");
                        
                        // GET /resources/{id}
                        var resource = await aquiferClient.Resources[1717].GetAsync(b =>
                        {
                            b.QueryParameters.ContentTextType = "html";
                        });
                        
                        Console.WriteLine($"""
                            Retrieved Resource:
                              - ID: {resource!.Id}
                              - Name: {resource.Name}
                              - Content: {await GetUntypedNodeOriginalJsonAsync(resource.Content)}
                            """);
                    }
                    catch (ErrorResponse ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                        Console.WriteLine($"Status Code: {ex.ResponseStatusCode}");
                        if (ex.Errors != null)
                        {
                            foreach (var additionalData in ex.Errors.AdditionalData)
                            {
                                Console.WriteLine($"\"{additionalData.Key}\": \"{await GetAdditionalDataValueAsStringAsync(additionalData.Value)}\"");
                            }
                        }
                        Console.WriteLine(ex.StackTrace);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                        Console.WriteLine(ex.StackTrace);
                    }
                    
                    private static async Task<string?> GetAdditionalDataValueAsStringAsync(object value)
                    {
                        if (value is UntypedNode untypedNode)
                        {
                            return await GetUntypedNodeOriginalJsonAsync(untypedNode);
                        }
                        
                        // TODO use your JSON serialization strategy of choice to render a better string
                        return value.ToString();
                    }
                    
                    private static async Task<string> GetUntypedNodeOriginalJsonAsync(UntypedNode untypedNode)
                    {
                        return await KiotaJsonSerializer.SerializeAsStringAsync(untypedNode);
                    }
                    ```
                    The following class is also used above in order to inject the API Key into every request header:
                    ```
                    public sealed class SetApiKeyHeaderRequestHandler(string _apiKey) : DelegatingHandler
                    {
                        protected override async Task<HttpResponseMessage> SendAsync(
                            HttpRequestMessage request, CancellationToken cancellationToken)
                        {
                            request.Headers.Add("api-key", _apiKey);
                            return await base.SendAsync(request, cancellationToken);
                        }
                    }
                    ```
                    See also [this C# Kiota example](https://github.com/microsoft/kiota-samples/blob/main/get-started/quickstart/dotnet/src/Program.cs) for how to use a generated Kiota client.
                    """");
                o.WithName("AquiferPublicAPIEndpointsClientsGetCsEndpoint");
                o.WithOpenApi();
            });

        app.MapApiClientEndpoint(
            $"/{ClientsRouteName}/java",
            c =>
            {
                c.SwaggerDocumentName = swaggerDocumentName;
                c.Language = GenerationLanguage.Java;
                c.ClientNamespaceName = "org.biblionexus.aquifer.api.client";
                c.ClientClassName = "AquiferClient";
                c.OutputPath = Path.Combine(c.OutputPath, "java", Path.GetRandomFileName());
                c.ExcludePatterns = [$"**/{ClientsRouteName}/**"];
            },
            o =>
            {
                o.CacheOutput(p => p.Expire(clientZipCacheDuration).Tag(ClientGenerationOutputCacheTag));
                o.WithTags("Clients");
                o.WithSummary("Download Java client.");
                o.WithDescription("""
                    Downloads a zip file containing a generated Java client to use when calling this API.
                    The generated source code uses [Kiota](https://learn.microsoft.com/en-us/openapi/kiota/) in order to make web requests. You will need to install the following dependencies:
                      * `com.microsoft.kiota:microsoft-kiota-bundle:1.8.0` (or newer version)
                      * `jakarta.annotation:jakarta.annotation-api:2.1.1` (or newer version)

                    See also [this Java Kiota example](https://github.com/microsoft/kiota-samples/blob/main/get-started/quickstart/java/app/src/main/java/kiotaposts/App.java) for how to use a generated Kiota client.
                    """);
                o.WithName("AquiferPublicAPIEndpointsClientsGetJavaEndpoint");
                o.WithOpenApi();
            });

        app.MapApiClientEndpoint(
            $"/{ClientsRouteName}/py",
            c =>
            {
                c.SwaggerDocumentName = swaggerDocumentName;
                c.Language = GenerationLanguage.Python;
                c.ClientNamespaceName = "biblionexus_aquifer_api_client";
                c.ClientClassName = "AquiferClient";
                c.OutputPath = Path.Combine(c.OutputPath, "py", Path.GetRandomFileName());
                c.ExcludePatterns = [$"**/{ClientsRouteName}/**"];
            },
            o =>
            {
                o.CacheOutput(p => p.Expire(clientZipCacheDuration).Tag(ClientGenerationOutputCacheTag));
                o.WithTags("Clients");
                o.WithSummary("Download Python client.");
                o.WithDescription("""
                    Downloads a zip file containing a generated Python client to use when calling this API.
                    The generated source code uses [Kiota](https://learn.microsoft.com/en-us/openapi/kiota/) in order to make web requests. You will need to install the following dependencies:
                      * `pip install microsoft-kiota-bundle==1.6.6` (or newer version)

                    See also [this Python Kiota example](https://github.com/microsoft/kiota-samples/blob/main/get-started/quickstart/python/main.py) for how to use a generated Kiota client.
                    """);
                o.WithName("AquiferPublicAPIEndpointsClientsGetPyEndpoint");
                o.WithOpenApi();
            });

        app.MapApiClientEndpoint(
            $"/{ClientsRouteName}/ts",
            c =>
            {
                c.SwaggerDocumentName = swaggerDocumentName;
                c.Language = GenerationLanguage.TypeScript;
                c.ClientNamespaceName = "biblionexus-aquifer-api-client";
                c.ClientClassName = "AquiferClient";
                c.OutputPath = Path.Combine(c.OutputPath, "ts", Path.GetRandomFileName());
                c.ExcludePatterns = [$"**/{ClientsRouteName}/**"];
            },
            o =>
            {
                o.CacheOutput(p => p.Expire(clientZipCacheDuration).Tag(ClientGenerationOutputCacheTag));
                o.WithTags("Clients");
                o.WithSummary("Download TypeScript client.");
                o.WithDescription("""
                    Downloads a zip file containing a generated TypeScript client to use when calling this API.
                    The generated source code uses [Kiota](https://learn.microsoft.com/en-us/openapi/kiota/) in order to make web requests. You will need to install the following dependencies:
                      * `npm install @microsoft/kiota-bundle@1.0.0-preview.77 -SE` (or newer version)

                    The following is a full example of an `index.tx` file using the generated client and injecting an API key header.
                    The example code assumes that the downloaded client code files are unzipped into a subdirectory named `client`.
                    ```
                    import { AnonymousAuthenticationProvider, SerializationWriter, serializeToJsonAsString, serializeUntypedNode, UntypedNode } from "@microsoft/kiota-abstractions";
                    import { FetchRequestAdapter, KiotaClientFactory, MiddlewareFactory } from "@microsoft/kiota-http-fetchlibrary";
                    import { createAquiferClient } from "./client/aquiferClient.js";
                    import { SetApiKeyHeaderRequestHandler } from "./SetApiKeyHeaderRequestHandler.js";
                    
                    // see https://learn.microsoft.com/en-us/openapi/kiota/middleware?tabs=typescript
                    const handlers = MiddlewareFactory.getDefaultMiddlewares();
                    handlers.unshift(new SetApiKeyHeaderRequestHandler("your-api-key-goes-here"));
                    const httpClient = KiotaClientFactory.create(undefined, handlers);
                    const adapter = new FetchRequestAdapter(new AnonymousAuthenticationProvider(), undefined, undefined, httpClient);
                    adapter.baseUrl = "https://api.aquifer.bible";
                    
                    const client = createAquiferClient(adapter);
                    
                    async function main(): Promise<void> {
                      try {
                        // GET /bibles/books
                        const bibleBooks = await client.bibles.books.get();
                        console.log(`Retrieved ${bibleBooks?.length} Bible books.`);
                    
                        // GET /bibles
                        const getBiblesResult = await client.bibles.get({
                            queryParameters: {
                                languageCode: "eng",
                                isLanguageDefault: true,
                            },
                        });
                        const englishLanguageDefaultBible = getBiblesResult?.[0];
                        console.log(`The English language default Bible is "${englishLanguageDefaultBible?.name}" (${englishLanguageDefaultBible?.abbreviation}).`);
                    
                        // GET /resources/{id}
                        const resource = await client.resources.byContentId(1717).get({
                            queryParameters: {
                                contentTextType: "html",
                            },
                        });
                        console.log(`Retrieved Resource:\n  - ID: ${resource?.id}\n  - Name: ${resource?.name}\n  - Content: ${getUntypedNodeOriginalJson(resource?.content!)}.`);
                    
                      } catch (error) {
                        console.log("Error:");
                        console.log(error);
                      }
                    }
                    
                    function getUntypedNodeOriginalJson(untypedNode: UntypedNode): string {
                        const serializeUntypedNodeToJson = (writer: SerializationWriter, value?: Partial<UntypedNode> | null): void => {
                            serializeUntypedNode(writer, value!);
                        }
                        return serializeToJsonAsString(untypedNode, serializeUntypedNodeToJson);
                    }
                    
                    main();
                    ```
                    The following class is also used above in order to inject the API Key into every request header:
                    ```
                    import { RequestOption } from "@microsoft/kiota-abstractions";
                    import { FetchRequestInit, Middleware } from "@microsoft/kiota-http-fetchlibrary";
                    
                    export class SetApiKeyHeaderRequestHandler implements Middleware {
                        public constructor(private readonly _apiKey: string) {}
                    
                        /** @inheritdoc */
                        next: Middleware | undefined;
                    
                        private setRequestHeader(options: FetchRequestInit | undefined, key: string, value: string): void {
                            if (options) {
                                if (!options.headers) {
                                    options.headers = {};
                                }
                                options.headers[key] = value;
                            }
                        };
                    
                        /** @inheritdoc */
                        public async execute(url: string, requestInit: RequestInit, requestOptions?: Record<string, RequestOption>): Promise<Response> {
                            this.setRequestHeader(requestInit as FetchRequestInit, "api-key", this._apiKey);
                    
                            const response = await this.next?.execute(url, requestInit, requestOptions);
                            if (!response) {
                                throw new Error("No response returned by the next middleware.");
                            }
                            return response;
                        }
                    }
                    ```
                    See also [this TypeScript Kiota example](https://github.com/microsoft/kiota-samples/blob/main/get-started/quickstart/typescript/index.ts) for how to use a generated Kiota client.
                    """);
                o.WithName("AquiferPublicAPIEndpointsClientsGetTsEndpoint");
                o.WithOpenApi();
            });

        return app;
    }
}