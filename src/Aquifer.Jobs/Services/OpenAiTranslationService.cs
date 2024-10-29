using Microsoft.Extensions.Logging;

namespace Aquifer.Jobs.Services;

public interface ITranslationService
{
    public Task<string> TranslateAsync(string text, CancellationToken cancellationToken);
}

public sealed class OpenAiTranslationService(
    ILogger<OpenAiTranslationService> _logger)
    : ITranslationService
{
    public async Task<string> TranslateAsync(string text, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Pretending to translate things.");
        await Task.Delay(100, cancellationToken);
        return text;
    }
}