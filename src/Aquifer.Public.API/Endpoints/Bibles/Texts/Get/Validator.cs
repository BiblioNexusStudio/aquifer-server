using Aquifer.Common.Utilities;
using Aquifer.Data.Enums;
using FastEndpoints;
using FluentValidation;

namespace Aquifer.Public.API.Endpoints.Bibles.Texts.Get;

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x.BookCode)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .Must(code => BibleBookCodeUtilities.IdFromCode(code) != BookId.None)
            .WithMessage("Invalid '{PropertyName}': \"{PropertyValue}\".");

        RuleFor(x => x.StartChapter)
            .LessThanOrEqualTo(x => x.EndChapter);
        RuleFor(x => x.StartVerse)
            .LessThanOrEqualTo(x => x.EndVerse)
            .When(x => x.StartChapter == x.EndChapter);

        RuleFor(x => x.StartChapter)
            .InclusiveBetween(1, 999);
        RuleFor(x => x.StartVerse)
            .InclusiveBetween(1, 999);
        RuleFor(x => x.EndChapter)
            .InclusiveBetween(1, 999);
        RuleFor(x => x.EndVerse)
            .InclusiveBetween(1, 999);
    }
}