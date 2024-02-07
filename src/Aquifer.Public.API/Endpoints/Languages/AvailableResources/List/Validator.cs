using Aquifer.Common.Utilities;
using Aquifer.Data.Enums;
using FastEndpoints;
using FluentValidation;

namespace Aquifer.Public.API.Endpoints.Languages.AvailableResources.List;

public class Validator : Validator<Request>
{
    public Validator()
    {
        // Should look into not having to duplicate this logic. It's also in /resources/Search/GetResources.
        RuleFor(x => x.BookCode).Must(x => x != null && BibleBookCodeUtilities.IdFromCode(x) != BookId.None)
            .WithMessage("Invalid book code {PropertyValue}. Get a valid list from /bible-books endpoint.");

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