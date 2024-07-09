using System.Text.RegularExpressions;
using Aquifer.API.Clients.Http.OpenAI;
using Aquifer.API.Common;
using Aquifer.API.Configuration;
using FastEndpoints;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Options;

namespace Aquifer.API.Endpoints.AI.Translate;

public partial class Endpoint(IOpenAiHttpClient openAiClient, IOptions<ConfigurationOptions> options) : Endpoint<Request>
{
    public override void Configure()
    {
        Post("ai/translate");
        Permissions(PermissionName.AiTranslate);
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        HttpContext.Features.Get<IHttpResponseBodyFeature>()?.DisableBuffering();
        HttpContext.Response.ContentType = "text/event-stream";
        var prompt = req.Prompt ?? $"{options.Value.OpenAiSettings.HtmlSimplifyBasePrompt} Then translate to {req.LanguageName}";

        var paragraphChunks = ParagraphRegex().Split(req.Content);
        foreach (var paragraphChunk in paragraphChunks)
        {
            List<string> sentenceSplits = [];
            if (paragraphChunk.Length > 5000)
            {
                sentenceSplits.AddRange(paragraphChunk.Trim().Split(". "));
            }
            else
            {
                sentenceSplits.Add(paragraphChunk);
            }

            for (var i = 0; i < sentenceSplits.Count; i++)
            {
                var sentence = sentenceSplits[i];
                if (i != sentenceSplits.Count - 1)
                {
                    sentence += ".&nbsp;";
                }

                using var clientResponse = await openAiClient.PostChatCompletionsAsync(prompt, sentence, ct);
                clientResponse.EnsureSuccessStatusCode();

                await using var stream = await clientResponse.Content.ReadAsStreamAsync(ct);
                await stream.CopyToAsync(HttpContext.Response.Body, ct);
                await HttpContext.Response.Body.FlushAsync(ct);
            }
        }
    }

    [GeneratedRegex("/(<h\\d|<p)/")]
    private static partial Regex ParagraphRegex();
}