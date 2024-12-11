namespace Aquifer.Jobs.Services.TranslationProcessors;

public sealed class FrenchTranslationProcessingService : ILanguageSpecificTranslationProcessingService
{
    public string Iso6393Code => "FRA";

    public Task<string> PostProcessHtmlAsync(string html, CancellationToken cancellationToken)
    {
        return Task.FromResult(html);
    }

    //private void
}