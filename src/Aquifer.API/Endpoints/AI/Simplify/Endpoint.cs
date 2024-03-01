using Aquifer.API.Clients.Http.OpenAI;
using Aquifer.API.Common;
using Aquifer.API.Configuration;
using Aquifer.Common.Utilities;
using FastEndpoints;
using Microsoft.Extensions.Options;

namespace Aquifer.API.Endpoints.AI.Simplify;

public class Endpoint(IOpenAiHttpClient openAiClient, IOptions<ConfigurationOptions> options, ILogger<Endpoint> logger)
    : Endpoint<Request, Response>
{
    public override void Configure()
    {
        Post("ai/simplify");
        Permissions(PermissionName.AiSimplify);
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        string? error = null;
        var chunks = HtmlUtilities.GetChunks(req.Content);
        List<string> newChunks = [];
        var prompt = $"{options.Value.OpenAiSettings.HtmlSimplifyBasePrompt} {req.Prompt}";

        foreach (var (name, html) in chunks)
        {
            if (name.StartsWith('h'))
            {
                newChunks.Add(html);
                continue;
            }

            var clientResponse = await openAiClient.PostChatCompletionsAsync(prompt, html, ct);
            var clientResponseContent = await clientResponse.Content.ReadAsStringAsync(ct);

            if (!clientResponse.IsSuccessStatusCode)
            {
                error = "Bad response code from OpenAI.";
                LogError(error, req, clientResponseContent);
                newChunks.Add(html);
                continue;
            }

            var chatCompletion = JsonUtilities.DefaultDeserialize<OpenAiChatCompletionResponse>(clientResponseContent);
            if (chatCompletion.Choices.Count == 0)
            {
                error = "No choices returned by OpenAI";
                LogError(error, req, clientResponseContent);
                newChunks.Add(html);
                continue;
            }

            var choice = chatCompletion.Choices[0];
            if (choice.FinishReason == "stop")
            {
                newChunks.Add(choice.Message.Content);
            }
            else
            {
                error = "Bad finish reason from OpenAI";
                LogError(error, req, clientResponseContent);
                newChunks.Add(html);
            }
        }

        var response = new Response
        {
            Content = string.Join("", newChunks),
            Error = error
        };

        await SendAsync(response, 200, ct);
    }

    private void LogError(string message, Request req, string responseContent)
    {
        logger.LogError("{message}. Prompt: {prompt} Request: {request}. Response: {response}",
            message,
            req.Prompt,
            req.Content,
            responseContent);
    }
}