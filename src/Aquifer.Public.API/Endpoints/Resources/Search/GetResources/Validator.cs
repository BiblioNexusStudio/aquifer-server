using Aquifer.Common.Utilities;
using Aquifer.Data.Entities;
using Aquifer.Data.Enums;
using FastEndpoints;
using FluentValidation;

namespace Aquifer.Public.API.Endpoints.Resources.Search.GetResources;

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x).Must(x => x.Query is not null || x.BookCode is not null || x.ResourceType is not ResourceType.None || x.ResourceCollectionCode is not null)
            .WithMessage("One of search query, bookCode, resourceType, or resourceCollectionCode is required.");

        RuleFor(x => x)
            .Must(x => (x.LanguageCode is not null && x.LanguageId == default) || (x.LanguageId != default && x.LanguageCode is null))
            .WithMessage("Either languageId or languageCode is required. Cannot use both.");

        RuleFor(x => x.BookCode).NotNull().When(x => x.StartChapter > 0);
        RuleFor(x => x.BookCode).Must(x => BibleBookCodeUtilities.IdFromCode(x!) != BookId.None)
            .WithMessage("Invalid book code {PropertyValue}. Get a valid list from /bibles/books endpoint.")
            .When(x => x.BookCode is not null);

        RuleFor(x => x).Must(x =>
                x.ResourceType == ResourceType.None ||
                (x.ResourceType != ResourceType.None && x.ResourceCollectionCode is null))
            .WithMessage("Cannot specify both resourceType and resourceCollectionCode.");

        RuleFor(x => x.StartChapter).InclusiveBetween(1, 150);
        RuleFor(x => x.EndChapter).InclusiveBetween(1, 150);
        RuleFor(x => x.StartVerse).InclusiveBetween(0, 200);
        RuleFor(x => x.EndVerse).InclusiveBetween(0, 200);

        RuleFor(x => x.StartChapter).NotNull().When(x => x.EndChapter != null || x.StartVerse != null);
        RuleFor(x => x.EndChapter).NotNull().When(x => x.StartChapter != null);
        RuleFor(x => x.StartVerse).NotNull().When(x => x.EndVerse != null);
        RuleFor(x => x.EndVerse).NotNull().When(x => x.StartVerse != null);

        RuleFor(x => x).Must(x => x.StartChapter <= x.EndChapter)
            .When(x => x.StartChapter != null && x.EndChapter != null)
            .WithMessage("startChapter cannot be greater than endChapter");

        RuleFor(x => x.Query).MinimumLength(3).When(x => x.Query is not null);
        RuleFor(x => x.ResourceType).IsInEnum();
        RuleFor(x => x.LanguageId).GreaterThan(0).When(x => x.LanguageCode is null);
        RuleFor(x => x.LanguageCode).Length(3).When(x => x.LanguageId == default);
        RuleFor(x => x.Offset).GreaterThanOrEqualTo(0);
        RuleFor(x => x.Limit).InclusiveBetween(0, 100);
    }
}