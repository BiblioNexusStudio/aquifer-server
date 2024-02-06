using FastEndpoints;
using FluentValidation;

namespace Aquifer.Public.API.Endpoints.Resources.Search.GetResources;

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x).Must(x =>
                (x.Query is not null || x.StartVerseId is not null || x.BookId is not null) && (x.StartVerseId is null || x.BookId is null))
            .WithMessage("Either searchQuery, startVerseId, or bookId is required. Cannot use both startVerseId and bookId.");

        RuleFor(x => x)
            .Must(x => (x.LanguageCode is not null && x.LanguageId == default) || (x.LanguageId != default && x.LanguageCode is null))
            .WithMessage("Either languageId or languageCode is required. Cannot use both.");

        RuleFor(x => x.Query).MinimumLength(3).When(x => x.Query is not null);
        RuleFor(x => x.ResourceType).IsInEnum();
        RuleFor(x => x.StartVerseId).InclusiveBetween(1001001001, 1099999999).When(x => x.StartVerseId is not null);
        RuleFor(x => x.EndVerseId).InclusiveBetween(1001001001, 1099999999).When(x => x.EndVerseId is not null);
        RuleFor(x => x.LanguageId).GreaterThan(0).When(x => x.LanguageCode is null);
        RuleFor(x => x.LanguageCode).Length(3).When(x => x.LanguageId == default);
        RuleFor(x => x.Offset).GreaterThanOrEqualTo(0);
        RuleFor(x => x.Limit).InclusiveBetween(0, 100);
        RuleFor(x => x.BookId).InclusiveBetween(1, 87).When(x => x.BookId is not null);
    }
}