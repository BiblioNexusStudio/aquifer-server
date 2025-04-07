using Aquifer.Common.Utilities;
using Aquifer.Data.Enums;
using FastEndpoints;
using FluentValidation;

namespace Aquifer.API.Endpoints.Resources.Untranslated.List;

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x)
            .Must(x => new[] { x.SearchQuery, x.BookCode }.Any(property => !string.IsNullOrEmpty(property)))
            .WithMessage("At least one of searchQuery or bookCode must be set.");
        RuleFor(x => x.SourceLanguageId).GreaterThan(0);
        RuleFor(x => x.TargetLanguageId).GreaterThan(0);
        RuleFor(x => x.ParentResourceId).GreaterThan(0);
        When(x => x.BookCode != null,
            () =>
                RuleFor(x => x.BookCode)
                    .Must(code => BibleBookCodeUtilities.IdFromCode(code!) != BookId.None)
                    .WithMessage("bookCode must be valid."));
        When(x => x.Chapters != null,
            () =>
                RuleForEach(x => x.Chapters)
                    .Must(chapter => chapter is >= 1 and <= 150)
                    .WithMessage("Chapters must be between 1 and 150."));
    }
}