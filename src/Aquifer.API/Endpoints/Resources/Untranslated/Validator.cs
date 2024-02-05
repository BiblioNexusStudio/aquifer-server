using Aquifer.API.Common;
using Aquifer.Data.Enums;
using FastEndpoints;
using FluentValidation;

namespace Aquifer.API.Endpoints.Resources.Untranslated;

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x)
            .Must(x => new[] { x.SearchQuery, x.BookCode }.Any(property => property != null))
            .WithMessage("At least one of searchQuery or bookCode must be set.");
        RuleFor(x => x.LanguageId).GreaterThan(0);
        RuleFor(x => x.ParentResourceId).GreaterThan(0);
        When(x => x.BookCode != null, () =>
            RuleFor(x => x.BookCode)
                .Must(code => BookCodes.IdFromCode(code!) != BookId.None)
                .WithMessage("bookCode must be valid.")
        );
        When(x => x.Chapters != null, () =>
            RuleForEach(x => x.Chapters)
                .Must(chapter => chapter is >= 1 and <= 150)
                .WithMessage("Chapters must be between 1 and 150.")
        );
    }
}