using Aquifer.Common.Utilities;
using Aquifer.Data.Enums;
using FastEndpoints;
using FluentValidation;

namespace Aquifer.API.Endpoints.Resources.Search;

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x).Must(x => x.Query is not null || x.BookCode is not null || x.Offset is not null)
            .WithMessage("Search query, bookCode or offset is required.");
        RuleFor(x => x.BookCode).Must(x => BibleBookCodeUtilities.IdFromCode(x!) != BookId.None)
            .WithMessage("Invalid book code {PropertyValue}. Get a valid list from /bible-books endpoint.")
            .When(x => x.BookCode is not null);
        RuleFor(x => x.BookCode).NotNull().When(x => x.StartChapter > 0);
        RuleFor(x => x.Query).MinimumLength(3).When(x => x.Query is not null);
        RuleFor(x => x.LanguageId).NotEmpty();
        RuleFor(x => x.ResourceTypes).NotEmpty();
        RuleForEach(x => x.ResourceTypes).IsInEnum();

        RuleFor(x => x.StartChapter).InclusiveBetween(0, 150);
        RuleFor(x => x.EndChapter).InclusiveBetween(0, 150);
        RuleFor(x => x.StartVerse).InclusiveBetween(0, 200);
        RuleFor(x => x.EndVerse).InclusiveBetween(0, 200);

        RuleFor(x => x.StartChapter).GreaterThan(0).When(x => x.EndChapter > 0 || x.StartVerse > 0);
        RuleFor(x => x.EndChapter).GreaterThan(0).When(x => x.StartChapter > 0);
        RuleFor(x => x.StartVerse).GreaterThan(0).When(x => x.EndVerse > 0);
        RuleFor(x => x.EndVerse).GreaterThan(0).When(x => x.StartVerse > 0);

        RuleFor(x => x).Must(x => x.StartChapter <= x.EndChapter)
            .WithMessage("startChapter cannot be greater than endChapter");
    }
}