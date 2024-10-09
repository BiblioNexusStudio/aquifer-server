using System.Text.RegularExpressions;
using Aquifer.API.Clients.Http.OpenAI;
using Aquifer.API.Common;
using Aquifer.API.Configuration;
using FastEndpoints;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Options;

namespace Aquifer.API.Endpoints.AI.Translate;

public partial class Endpoint(
    IOpenAiHttpClient openAiClient,
    IOptions<ConfigurationOptions> options,
    IConfiguration configuration) : Endpoint<Request>
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

        var prompt = req.Prompt ?? GetPrompt(req);
        var paragraphChunks = ParagraphRegex().Split(req.Content);
        paragraphChunks = paragraphChunks.Where(x => !string.IsNullOrWhiteSpace(x) && x.Length > 2).ToArray();

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

                await StreamCompletionToResponseBody(prompt, sentence, ct);
            }
        }
    }

    private async Task StreamCompletionToResponseBody(string prompt, string sentence, CancellationToken ct, int retries = 0)
    {
        try
        {
            using var clientResponse = await openAiClient.PostChatCompletionsAsync(prompt, sentence, ct);
            clientResponse.EnsureSuccessStatusCode();

            await using var stream = await clientResponse.Content.ReadAsStreamAsync(ct);
            await stream.CopyToAsync(HttpContext.Response.Body, ct);
            await HttpContext.Response.Body.FlushAsync(ct);
        }
        catch (Exception)
        {
            if (++retries < 3)
            {
                // Wait a moment before trying again
                await Task.Delay(500, ct);
                await StreamCompletionToResponseBody(prompt, sentence, ct, retries + 1);
            }
            else
            {
                throw;
            }
        }
    }

    private string GetPrompt(Request req)
    {
        string? languagePrompt = null;
        if (req.LanguageCode is not null)
        {
            languagePrompt = configuration.GetSection($"LanguagePrompts:{req.LanguageCode}").Value;
        }

        languagePrompt ??= $"Then translate to {req.LanguageName}";
        return $"{options.Value.OpenAiSettings.HtmlSimplifyBasePrompt} {languagePrompt}";
    }

    [GeneratedRegex("(?=<([hH][1-6]|[pP])\\b[^>]*>)")]
    private static partial Regex ParagraphRegex();
}