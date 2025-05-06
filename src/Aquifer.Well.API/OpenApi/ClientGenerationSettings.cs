using FastEndpoints.ClientGen.Kiota;
using Kiota.Builder;

namespace Aquifer.Well.API.OpenApi;

/// <summary>
/// Generate client code and drop it into the BibleWell repo locally.
/// See https://fast-endpoints.com/docs/swagger-support#save-to-disk-with-app-run.
/// Generate the client with <code>dotnet run --project src/Aquifer.Well.API --generateclients true</code>.
/// We could also generate clients on build if desired.
/// </summary>
public static class ClientGenerationSettings
{
    public static async Task GenerateApiClientAsync(
        this IHost app,
        string swaggerDocumentName)
    {
        await app.GenerateApiClientsAndExitAsync(c =>
        {
            c.SwaggerDocumentName = swaggerDocumentName;
            c.Language = GenerationLanguage.CSharp;
            c.ClientNamespaceName = "BibleWell.Aquifer.API.Client";
            c.ClientClassName = "AquiferWellApiClient";
            c.OutputPath = Path.Combine(c.OutputPath, "../../../../bible-well/src/BibleWell.Aquifer.Api/Client");
        });
    }
}