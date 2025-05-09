using Aquifer.Common.Utilities;
using Aquifer.Data.Enums;
using FastEndpoints;
using FluentValidation;

namespace Aquifer.API.Endpoints.Resources.Search;

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x)
            .Must(x => x.Query is not null || x.BookCode is not null || x.ParentResourceId is not null)
            .WithMessage("Search query, bookCode or parentResourceId is required.");
        RuleFor(x => x.BookCode)
            .Must(x => BibleBookCodeUtilities.IdFromCode(x!) != BookId.None)
            .WithMessage("Invalid book code {PropertyValue}. Get a valid list from /bible-books endpoint.")
            .When(x => x.BookCode is not null);
        RuleFor(x => x.BookCode).NotNull().When(x => x.StartChapter > 0);
        RuleFor(x => x.Query).MinimumLength(3).When(x => x.Query is not null);
        RuleFor(x => x.LanguageId).NotEmpty();
        RuleFor(x => x.ResourceTypes).NotEmpty().When(x => x.ParentResourceId is null);
        RuleForEach(x => x.ResourceTypes).IsInEnum();

        RuleFor(x => x.StartChapter).InclusiveBetween(1, 150);
        RuleFor(x => x.EndChapter).InclusiveBetween(1, 150);
        RuleFor(x => x.StartVerse).InclusiveBetween(0, 200);
        RuleFor(x => x.EndVerse).InclusiveBetween(0, 200);

        RuleFor(x => x.StartChapter).NotNull().When(x => x.EndChapter != null || x.StartVerse != null);
        RuleFor(x => x.EndChapter).NotNull().When(x => x.StartChapter != null);
        RuleFor(x => x.StartVerse).NotNull().When(x => x.EndVerse != null);
        RuleFor(x => x.EndVerse).NotNull().When(x => x.StartVerse != null);

        RuleFor(x => x)
            .Must(x => x.StartChapter <= x.EndChapter)
            .When(x => x.StartChapter != null && x.EndChapter != null)
            .WithMessage("startChapter cannot be greater than endChapter");
    }
}