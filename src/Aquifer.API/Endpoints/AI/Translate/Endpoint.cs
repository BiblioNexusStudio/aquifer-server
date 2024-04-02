using Aquifer.API.Clients.Http.OpenAI;
using Aquifer.API.Common;
using Aquifer.API.Configuration;
using Aquifer.Common.Utilities;
using FastEndpoints;
using Microsoft.Extensions.Options;

namespace Aquifer.API.Endpoints.AI.Translate;

public class Endpoint(IOpenAiHttpClient openAiClient, IOptions<ConfigurationOptions> options, ILogger<Endpoint> logger)
    : Endpoint<Request, Response>
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

        var clientResponse = await openAiClient.PostChatCompletionsAsync(prompt, req.Content, ct);
        var clientResponseContent = await clientResponse.Content.ReadAsStringAsync(ct);

        if (!clientResponse.IsSuccessStatusCode)
        {
            error = "Bad response code from OpenAI.";
            LogError(error, req, clientResponseContent);
            ThrowError(error, 500);
        }

        var chatCompletion = JsonUtilities.DefaultDeserialize<OpenAiChatCompletionResponse>(clientResponseContent);
        if (chatCompletion.Choices.Count == 0)
        {
            error = "No choices returned by OpenAI";
            LogError(error, req, clientResponseContent);
            ThrowError(error, 500);
        }

        var choice = chatCompletion.Choices[0];
        if (choice.FinishReason != "stop")
        {
            error = "Bad finish reason from OpenAI";
            LogError(error, req, clientResponseContent);
            ThrowError(error, 500);
        }

        var response = new Response
        {
            Content = choice.Message.Content,
            Error = error
        };

        await SendAsync(response, 200, ct);
    }

    private void LogError(string message, Request req, string responseContent)
    {
        logger.LogError("{message}. LanguageName: {languageName} Content: {content}. Response: {response}",
            message,
            req.LanguageName,
            req.Content,
            responseContent);
    }
}