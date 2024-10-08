﻿using System.Text.Json.Serialization;

namespace Aquifer.API.Clients.Http.OpenAI;

public class OpenAiChatCompletionRequest
{
    public string Model { get; set; } = "gpt-4o";
    public float Temperature { get; set; } = 0.2f;
    public List<OpenAiChatCompletionRequestMessage> Messages { get; set; } = [];
    public bool Stream { get; set; } = true;
}

public class OpenAiChatCompletionRequestMessage
{
    public string? Role { get; set; }
    public string? Content { get; set; }
}

public class OpenAiChatCompletionResponse
{
    // unless request specifies n > 1, this will always be length 1
    public List<OpenAiChatCompletionResponseChoices> Choices { get; set; } = [];
}

public class OpenAiChatCompletionResponseChoices
{
    public int Index { get; set; }
    public OpenAiChatCompletionRequestMessage Delta { get; set; } = null!;

    [JsonPropertyName("finish_reason")]
    public string FinishReason { get; set; } = null!;
}

public class OpenAiChatCompletionResponseUsage
{
    [JsonPropertyName("prompt_tokens")]
    public int PromptTokens { get; set; }

    [JsonPropertyName("completion_tokens")]
    public int CompletionTokens { get; set; }

    [JsonPropertyName("total_tokens")]
    public int TotalTokens { get; set; }
}