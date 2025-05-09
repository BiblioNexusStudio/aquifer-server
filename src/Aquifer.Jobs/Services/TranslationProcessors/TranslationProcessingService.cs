using System.Collections.ObjectModel;
using Microsoft.Extensions.DependencyInjection;

namespace Aquifer.Jobs.Services.TranslationProcessors;

public interface ITranslationProcessingService
{
    Task<string> PostProcessHtmlAsync(string html, string languageIso6393Code, CancellationToken cancellationToken);
}

public interface ILanguageSpecificTranslationProcessingService
{
    string Iso6393Code { get; }
    Task<string> PostProcessHtmlAsync(string html, CancellationToken cancellationToken);
}

public static class TranslationProcessingServicesRegistry
{
    public static IServiceCollection AddTranslationProcessingServices(this IServiceCollection services)
    {
        services.AddSingleton<ITranslationProcessingService, TranslationProcessingService>();

        foreach (var implementationType in AppDomain.CurrentDomain
            .GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(t => typeof(ILanguageSpecificTranslationProcessingService).IsAssignableFrom(t) &&
                t is { IsClass: true, IsAbstract: false }))
        {
            services.AddSingleton(typeof(ILanguageSpecificTranslationProcessingService), implementationType);
        }

        return services;
    }
}

public sealed class TranslationProcessingService(
    IEnumerable<ILanguageSpecificTranslationProcessingService> _languageSpecificTranslationProcessingServices)
    : ITranslationProcessingService
{
    private readonly ReadOnlyDictionary<string, ILanguageSpecificTranslationProcessingService>
        _languageSpecificTranslationProcessingServiceByIso6393CodeMap = new(
            _languageSpecificTranslationProcessingServices.ToDictionary(s => s.Iso6393Code, StringComparer.OrdinalIgnoreCase));

    public async Task<string> PostProcessHtmlAsync(string html, string languageIso6393Code, CancellationToken cancellationToken)
    {
        var languageSpecificTranslationProcessingService =
            _languageSpecificTranslationProcessingServiceByIso6393CodeMap.GetValueOrDefault(languageIso6393Code);

        return languageSpecificTranslationProcessingService == null
            ? html
            : await languageSpecificTranslationProcessingService.PostProcessHtmlAsync(html, cancellationToken);
    }
}