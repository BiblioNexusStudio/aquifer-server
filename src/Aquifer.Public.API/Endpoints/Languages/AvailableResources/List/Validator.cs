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
        RuleFor(x => x.BookCode)
            .Must(x => x != null && BibleBookCodeUtilities.IdFromCode(x) != BookId.None)
            .WithMessage("Invalid book code {PropertyValue}. Get a valid list from /bibles/books endpoint.");

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

        RuleForEach(x => x.LanguageCodes)
            .Must(languageCode => languageCode.Length == 3)
            .WithMessage("Language code '{PropertyValue}' must be 3 characters long.");
    }
}