using Aquifer.API.Clients.Http.OpenAI;
using Aquifer.API.Configuration;
using Aquifer.Common.Utilities;
using FastEndpoints;
using Microsoft.Extensions.Options;

namespace Aquifer.API.Endpoints.AI.Simplify;

public class Endpoint(IOpenAiHttpClient openAiClient, IOptions<ConfigurationOptions> options, ILogger<Endpoint> logger) : Endpoint<Request>
{
    public override void Configure()
    {
        Post("ai/simplify");
        //Permissions(PermissionName.AiSimplify);
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var chunks = HtmlUtilities.GetChunks(req.Content);
        List<string> simplifiedChunks = [];
        var hadProblem = false;
        var prompt = $"{options.Value.OpenAiSettings.HtmlSimplifyBasePrompt} {req.Prompt}";

        foreach (var (name, html) in chunks)
        {
            if (name.StartsWith('h'))
            {
                simplifiedChunks.Add(html);
                continue;
            }

            var response = await openAiClient.PostChatCompletionsAsync(prompt, html, ct);
            var responseContent = await response.Content.ReadAsStringAsync(ct);

            if (!response.IsSuccessStatusCode)
            {
                hadProblem = true;
                LogError("Bad response code from OpenAI.", req, responseContent);
                simplifiedChunks.Add(html);
                continue;
            }

            var chatCompletion = JsonUtilities.DefaultDeserialize<OpenAiChatCompletionResponse>(responseContent);
            if (chatCompletion.Choices.Count == 0)
            {
                hadProblem = true;
                LogError("No choices returned by OpenAI", req, responseContent);
                simplifiedChunks.Add(html);
                continue;
            }

            var choice = chatCompletion.Choices[0];
            if (choice.FinishReason == "stop")
            {
                simplifiedChunks.Add(choice.Message.Content);
            }
            else
            {
                hadProblem = true;
                LogError("Bad finish reason from OpenAI", req, responseContent);
                simplifiedChunks.Add(html);
            }
        }

        var joinedChunks = string.Join("", simplifiedChunks);
        await SendStringAsync(joinedChunks, hadProblem ? 424 : 200, cancellation: ct);
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