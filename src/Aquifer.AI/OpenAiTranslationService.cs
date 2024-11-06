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
    public string? TextTranslationPromptOverride { get; init; }
    public string? HtmlTranslationBasePromptOverride { get; init; }
    public IReadOnlyDictionary<string, string>? LanguageSpecificPromptAppendixByLanguageIso6393CodeMap { get; init; }
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
            new ChatCompletionOptions(),
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
                    minifiedHtml => TranslateHtmlChunkAsync(_chatClient, prompt, minifiedHtml, cancellationToken)))
                .ToList();

            await Task.WhenAll(paragraphTranslationTasks);

            foreach (var paragraphTranslationTask in paragraphTranslationTasks)
            {
                translatedHtml.Append(paragraphTranslationTask.Result);
            }
        }

        return translatedHtml.ToString();

        static async Task<string> TranslateHtmlChunkAsync(ChatClient chatClient, string prompt, string html, CancellationToken ct)
        {
            var chatCompletion = await chatClient.CompleteChatAsync(
                [
                    ChatMessage.CreateSystemMessage(prompt),
                    ChatMessage.CreateUserMessage(html),
                ],
                new ChatCompletionOptions(),
                ct);

            if (chatCompletion.Value.FinishReason != ChatFinishReason.Stop)
            {
                throw new OpenAiChatCompletionException(
                    $"OpenAI chat completion returned an unhandled finish reason: {chatCompletion.Value.FinishReason}.",
                    prompt,
                    html);
            }

            return chatCompletion.Value.Content[0].Text.Replace(Environment.NewLine, "");
        }
    }

    private string GetHtmlTranslationPrompt((string Iso6393Code, string EnglishName) destinationLanguage)
    {
        const string defaultHtmlTranslationBasePrompt = """
            You receive HTML and then return the HTML with an altered version of the text therein.
            Never change the formatting of the HTML elements or attributes.
            For example, if text has an <em> tag around it or a <span> tag with attributes, that tag and its attributes should stay with the text even if it is simplified or moved within the text.
            Keep the HTML tags and attributes no matter what.
            Never add a missing html element (i.e. if something starts with <p> but the end is missing </p>, don't add it).
            Never remove '&nbsp;'.
            Never echo the original content, only respond with the translation.
            """;

        var basePrompt = _options.HtmlTranslationBasePromptOverride
            ?? defaultHtmlTranslationBasePrompt;

        var languageSpecificPromptAppendix =
            _options.LanguageSpecificPromptAppendixByLanguageIso6393CodeMap
                ?.GetValueOrDefault(destinationLanguage.Iso6393Code.ToUpper())
            ?? $"Then translate to {destinationLanguage.EnglishName}.";

        return $"{basePrompt} {languageSpecificPromptAppendix}";
    }

    private string GetTextTranslationPrompt((string Iso6393Code, string EnglishName) destinationLanguage)
    {
        return _options.TextTranslationPromptOverride
            ?? $"""
                You receive a string and then return that string in the ${destinationLanguage.EnglishName} language.
                Translate the exact string that you receive.
                Do not interpret it as anything other than the exact string that it is.
                """;
    }

    [GeneratedRegex("(?=<([hH][1-6]|[pP])\\b[^>]*>)")]
    private static partial Regex ParagraphRegex();
}