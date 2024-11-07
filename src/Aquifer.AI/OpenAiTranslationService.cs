using System.Text;
using System.Text.RegularExpressions;
using Aquifer.Common.Clients;
using Aquifer.Common.Utilities;
using OpenAI.Chat;

namespace Aquifer.AI;

public interface ITranslationService
{
    /// <summary>
    /// Translates plain text.
    /// Note: It's up to the caller to guarantee that the text length is not too long.
    /// </summary>
    public Task<string> TranslateTextAsync(
        string text,
        (string Iso6393Code, string EnglishName) destinationLanguage,
        CancellationToken cancellationToken);

    /// <summary>
    /// Translates HTML content while preserving the HTML tags.
    /// Note: Translations are done on each individual paragraph in order to avoid hitting length limitations.
    /// </summary>
    public Task<string> TranslateHtmlAsync(
        string html,
        (string Iso6393Code, string EnglishName) destinationLanguage,
        CancellationToken cancellationToken);
}

public class OpenAiOptions
{
    public required string Model { get; init; }
}

public sealed class OpenAiTranslationOptions
{
    public required string DefaultLanguageSpecificPromptAppendixFormatString { get; init; }
    public required string HtmlTranslationBasePrompt { get; init; }
    public required Dictionary<string, string> LanguageSpecificPromptAppendixByLanguageIso6393CodeMap { get; init; }
    public required float Temperature { get; init; }
    public required string TextTranslationPromptFormatString { get; init; }
}

public sealed class OpenAiChatCompletionException(string message, string prompt, string text)
    : Exception(message)
{
    public string Prompt { get; } = prompt;
    public string Text { get; } = text;
}

public sealed partial class OpenAiTranslationService : ITranslationService
{
    private const int _maxContentLength = 5_000;
    private const int _maxParallelizationForSingleTranslation = 5;

    private readonly ChatClient _chatClient;
    private readonly ChatCompletionOptions _chatCompletionOptions;

    private readonly OpenAiTranslationOptions _options;

    public OpenAiTranslationService(
        OpenAiTranslationOptions openAiTranslationOptions,
        OpenAiOptions openAiOptions,
        IAzureKeyVaultClient keyVaultClient)
    {
        _options = openAiTranslationOptions;

        // TODO Inject a ChatClient instead of building one here. This will require changing how we fetch key vault secrets.
        const string openAiApiKeySecretName = "OpenAiApiKey";
        var openApiKey = keyVaultClient.GetSecretAsync(openAiApiKeySecretName).GetAwaiter().GetResult();

        _chatClient = new ChatClient(openAiOptions.Model, openApiKey);
        _chatCompletionOptions = new ChatCompletionOptions
        {
            Temperature = _options.Temperature,
        };
    }

    public async Task<string> TranslateTextAsync(
        string text,
        (string Iso6393Code, string EnglishName) destinationLanguage,
        CancellationToken cancellationToken)
    {
        if (text.Length > _maxContentLength)
        {
            throw new ArgumentException(
                $"{nameof(text)} must have fewer than {_maxContentLength} characters but has {text.Length}.",
                nameof(text));
        }

        var prompt = GetTextTranslationPrompt(destinationLanguage);

        var chatCompletion = await _chatClient.CompleteChatAsync(
            [
                ChatMessage.CreateSystemMessage(prompt),
                ChatMessage.CreateUserMessage(text),
            ],
            _chatCompletionOptions,
            cancellationToken);

        if (chatCompletion.Value.FinishReason != ChatFinishReason.Stop)
        {
            throw new OpenAiChatCompletionException(
                $"OpenAI chat completion returned an unhandled finish reason: {chatCompletion.Value.FinishReason}.",
                prompt,
                text);
        }

        return chatCompletion.Value.Content[0].Text;
    }

    public async Task<string> TranslateHtmlAsync(
        string html,
        (string Iso6393Code, string EnglishName) destinationLanguage,
        CancellationToken cancellationToken)
    {
        var prompt = GetHtmlTranslationPrompt(destinationLanguage);

        var translatedHtml = new StringBuilder();

        // process the translations in multiple parallel batches
        foreach (var paragraphs in ParagraphRegex()
            .Split(html)
            .Where(x => !string.IsNullOrWhiteSpace(x) && x.Length > 2)
            .Chunk(_maxParallelizationForSingleTranslation))
        {
            // Translate paragraphs in each batch in parallel,
            // but wait for all paragraphs in the batch to finish before starting the next batch.
            var paragraphTranslationTasks = paragraphs
                .Select(paragraph => HtmlUtilities.ProcessHtmlContentAsync(
                    paragraph,
                    minifiedHtml => TranslateHtmlChunkAsync(prompt, minifiedHtml, cancellationToken)))
                .ToList();

            await Task.WhenAll(paragraphTranslationTasks);

            foreach (var paragraphTranslationTask in paragraphTranslationTasks)
            {
                translatedHtml.Append(paragraphTranslationTask.Result);
            }
        }

        return translatedHtml.ToString();
    }

    private async Task<string> TranslateHtmlChunkAsync(string prompt, string html, CancellationToken cancellationToken)
    {
        var chatCompletion = await _chatClient.CompleteChatAsync(
            [
                ChatMessage.CreateSystemMessage(prompt),
                ChatMessage.CreateUserMessage(html),
            ],
            _chatCompletionOptions,
            cancellationToken);

        if (chatCompletion.Value.FinishReason != ChatFinishReason.Stop)
        {
            throw new OpenAiChatCompletionException(
                $"OpenAI chat completion returned an unhandled finish reason: {chatCompletion.Value.FinishReason}.",
                prompt,
                html);
        }

        return chatCompletion.Value.Content[0].Text.Replace(Environment.NewLine, "");
    }

    private string GetHtmlTranslationPrompt((string Iso6393Code, string EnglishName) destinationLanguage)
    {
        var languageSpecificPromptAppendix =
            _options.LanguageSpecificPromptAppendixByLanguageIso6393CodeMap
                .GetValueOrDefault(destinationLanguage.Iso6393Code.ToUpper())
            ?? string.Format(_options.DefaultLanguageSpecificPromptAppendixFormatString, destinationLanguage.EnglishName);

        return $"{_options.HtmlTranslationBasePrompt} {languageSpecificPromptAppendix}";
    }

    private string GetTextTranslationPrompt((string Iso6393Code, string EnglishName) destinationLanguage)
    {
        return string.Format(_options.TextTranslationPromptFormatString, destinationLanguage.EnglishName);
    }

    [GeneratedRegex("(?=<([hH][1-6]|[pP])\\b[^>]*>)")]
    private static partial Regex ParagraphRegex();
}