using System.Collections.ObjectModel;
using Microsoft.Extensions.DependencyInjection;

namespace Aquifer.Jobs.Services;

public interface ITranslationPostProcessingService
{
    public Task<string> PostProcessHtmlAsync(string html, string languageIso6393Code, CancellationToken cancellationToken);
}

public interface ILanguageSpecificTranslationPostProcessingService
{
    public string Iso6393Code { get; }
    public Task<string> PostProcessHtmlAsync(string html, CancellationToken cancellationToken);
}

public static class TranslationPostProcessingServicesRegistry
{
    public static IServiceCollection AddTranslationPostProcessingServices(this IServiceCollection services)
    {
        services.AddSingleton<ITranslationPostProcessingService, TranslationPostProcessingService>();

        foreach (var implementationType in AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes())
            .Where(t =>
                typeof(ILanguageSpecificTranslationPostProcessingService).IsAssignableFrom(t) &&
                t is { IsClass: true, IsAbstract: false }))
        {
            services.AddSingleton(typeof(ILanguageSpecificTranslationPostProcessingService), implementationType);
        }

        return services;
    }
}

public sealed class TranslationPostProcessingService(
    IEnumerable<ILanguageSpecificTranslationPostProcessingService> _languageSpecificTranslationPostProcessingServices)
    : ITranslationPostProcessingService
{
    private readonly ReadOnlyDictionary<string, ILanguageSpecificTranslationPostProcessingService>
        _languageSpecificTranslationPostProcessingServiceByIso6393CodeMap = new(
            _languageSpecificTranslationPostProcessingServices
                .ToDictionary(s => s.Iso6393Code, StringComparer.OrdinalIgnoreCase));

    public async Task<string> PostProcessHtmlAsync(string html, string languageIso6393Code, CancellationToken cancellationToken)
    {
        var languageSpecificTranslationPostProcessingService =
            _languageSpecificTranslationPostProcessingServiceByIso6393CodeMap.GetValueOrDefault(languageIso6393Code);

        return languageSpecificTranslationPostProcessingService == null
            ? html
            : await languageSpecificTranslationPostProcessingService.PostProcessHtmlAsync(html, cancellationToken);
    }
}

public sealed class EnglishTranslationPostProcessingService : ILanguageSpecificTranslationPostProcessingService
{
    public string Iso6393Code => "ENG";

    public Task<string> PostProcessHtmlAsync(string html, CancellationToken cancellationToken)
    {
        return Task.FromResult(html);
    }
}

public sealed class FrenchTranslationPostProcessingService : ILanguageSpecificTranslationPostProcessingService
{
    public string Iso6393Code => "FRA";

    public Task<string> PostProcessHtmlAsync(string html, CancellationToken cancellationToken)
    {
        return Task.FromResult(html);
    }
}