using System.Text.RegularExpressions;
using Aquifer.API.Clients.Http.OpenAI;
using Aquifer.API.Common;
using Aquifer.API.Configuration;
using FastEndpoints;
using Microsoft.Extensions.Options;

namespace Aquifer.API.Endpoints.AI.Translate;

public partial class Endpoint(
    IOpenAiHttpClient openAiClient,
    IOptions<ConfigurationOptions> options,
    ILogger<Endpoint> logger) : Endpoint<Request>
{
    public override void Configure()
    {
        Post("ai/translate");
        Permissions(PermissionName.AiTranslate);
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        string? error = null;
        var prompt = $"{options.Value.OpenAiSettings.HtmlSimplifyBasePrompt} Then translate to {req.LanguageName}";

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
                    sentence += ". ";
                }

                using var clientResponse = await openAiClient.PostChatCompletionsAsync(prompt, sentence, ct);
                clientResponse.EnsureSuccessStatusCode();

                await using var stream = await clientResponse.Content.ReadAsStreamAsync(ct);
                //await SendStreamAsync(stream, enableRangeProcessing: true, cancellation: ct);
                await stream.CopyToAsync(HttpContext.Response.Body, ct);
                await HttpContext.Response.Body.FlushAsync(ct);

                // await HttpContext.Response.Body.wr
            }
        }

        //ThrowError("whatever", 500);

        // if (!clientResponse.IsSuccessStatusCode)
        // {
        //     error = "Bad response code from OpenAI.";
        //     LogError(error, req, clientResponseContent);
        //     ThrowError(error, 500);
        // }
        //
        // var chatCompletion = JsonUtilities.DefaultDeserialize<OpenAiChatCompletionResponse>(clientResponseContent);
        // if (chatCompletion.Choices.Count == 0)
        // {
        //     error = "No choices returned by OpenAI";
        //     LogError(error, req, clientResponseContent);
        //     ThrowError(error, 500);
        // }
        //
        // var choice = chatCompletion.Choices[0];
        // if (choice.FinishReason != "stop")
        // {
        //     error = "Bad finish reason from OpenAI";
        //     LogError(error, req, clientResponseContent);
        //     ThrowError(error, 500);
        // }

        // var response = new Response
        // {
        //     Content = choice.Message.Content,
        //     Error = error
        // };

        // await SendAsync(response, 200, ct);
    }

    private void LogError(string message, Request req, string responseContent)
    {
        logger.LogError("{message}. LanguageName: {languageName} Content: {content}. Response: {response}",
            message,
            req.LanguageName,
            req.Content,
            responseContent);
    }

    [GeneratedRegex("/(<h\\d|<p)/")]
    private static partial Regex ParagraphRegex();
}