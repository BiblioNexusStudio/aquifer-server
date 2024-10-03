using Aquifer.Common.Utilities;
using Aquifer.Data.Enums;
using FastEndpoints;
using FluentValidation;

namespace Aquifer.Public.API.Endpoints.Bibles.Alignments.Greek;

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x.BookCode)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .Must(code => BibleBookCodeUtilities.IdFromCode(code) != BookId.None)
            .WithMessage("Invalid '{PropertyName}': \"{PropertyValue}\".")
            .Must(code => BibleBookCodeUtilities.IdFromCode(code) is
                >= BookId.BookMAT and
                <= BookId.BookREV)
            .WithMessage("Invalid '{PropertyName}': \"{PropertyValue}\". Must be a valid New Testament book.");

        RuleFor(x => x.StartChapter)
            .LessThanOrEqualTo(x => x.EndChapter);
        RuleFor(x => x.StartVerse)
            .LessThanOrEqualTo(x => x.EndVerse)
            .When(x => x.StartChapter == x.EndChapter);
        RuleFor(x => x.StartWord)
            .LessThanOrEqualTo(x => x.EndWord)
            .When(x => x.StartVerse == x.EndVerse);

        RuleFor(x => x.StartChapter)
            .InclusiveBetween(1, 999);
        RuleFor(x => x.StartVerse)
            .InclusiveBetween(1, 999);
        RuleFor(x => x.StartWord)
            .InclusiveBetween(1, 999);
        RuleFor(x => x.EndChapter)
            .InclusiveBetween(1, 999);
        RuleFor(x => x.EndVerse)
            .InclusiveBetween(1, 999);
        RuleFor(x => x.EndWord)
            .InclusiveBetween(1, 999);
    }
}
