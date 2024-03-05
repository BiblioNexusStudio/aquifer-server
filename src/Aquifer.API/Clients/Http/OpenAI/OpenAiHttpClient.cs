﻿using Aquifer.API.Common;
using Aquifer.API.Configuration;
using Aquifer.Common.Utilities;
using Microsoft.Extensions.Options;

namespace Aquifer.API.Clients.Http.OpenAI;

public interface IOpenAiHttpClient
{
    Task<HttpResponseMessage> PostChatCompletionsAsync(string prompt, string content, CancellationToken ct);
}

public class OpenAiHttpClient : IOpenAiHttpClient
{
    private readonly HttpClient _httpClient;
    private readonly OpenAiSettings _openAiSettings;

    public OpenAiHttpClient(HttpClient httpClient,
        IOptions<ConfigurationOptions> options,
        IAzureKeyVaultClient keyVaultClient)
    {
        _httpClient = httpClient;
        _openAiSettings = options.Value.OpenAiSettings;

        _httpClient.BaseAddress = new Uri(_openAiSettings.BaseUri);
        _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization",
            $"Bearer {keyVaultClient.GetSecretAsync(KeyVaultSecretName.OpenAiApiKey).GetAwaiter().GetResult()}");
    }

    public async Task<HttpResponseMessage> PostChatCompletionsAsync(string prompt, string content, CancellationToken ct)
    {
        var request = new OpenAiChatCompletionRequest
        {
            Messages =
            [
                new OpenAiChatCompletionRequestMessage
                {
                    Role = "system",
                    Content = prompt
                },
                new OpenAiChatCompletionRequestMessage
                {
                    Role = "user",
                    Content = content
                }
            ]
        };

        return await _httpClient.PostAsJsonAsync(_openAiSettings.ChatCompletionsPath, request, JsonUtilities.DefaultOptions, ct);
    }
}