using Aquifer.Common.Utilities;
using Aquifer.Data.Enums;
using FastEndpoints;
using FluentValidation;

namespace Aquifer.Public.API.Endpoints.Bibles.Texts.Get;

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x.BookCode).NotEmpty();
        RuleFor(x => x.BookCode)
            .Must(code => BibleBookCodeUtilities.IdFromCode(code) != BookId.None)
            .When(x => x.BookCode is not null)
            .WithMessage(x => "Invalid '{PropertyName}': \"{PropertyValue}\".");

        RuleFor(x => x.StartChapter)
            .NotEmpty()
            .When(x => x.StartVerse is not null)
            .WithMessage(x => "'{PropertyName}' must also be provided when 'Start Verse' is provided.");
        RuleFor(x => x.EndChapter)
            .NotEmpty()
            .When(x => x.EndVerse is not null)
            .WithMessage(x => "'{PropertyName}' must also be provided when 'End Verse' is provided.");

        RuleFor(x => x.StartChapter)
            .InclusiveBetween(1, 999);
        RuleFor(x => x.StartVerse)
            .InclusiveBetween(1, 999);
        RuleFor(x => x.EndChapter)
            .InclusiveBetween(1, 999);
        RuleFor(x => x.EndVerse)
            .InclusiveBetween(1, 999);

        When(
            x => x.StartChapter.HasValue && x.EndChapter.HasValue,
            () =>
            {
                RuleFor(x => x.StartChapter)
                    .LessThanOrEqualTo(x => x.EndChapter)
                    .WithMessage(x => "'{PropertyName}' must be less than or equal to '{ComparisonProperty}'.");

                RuleFor(x => x.StartVerse)
                    .LessThanOrEqualTo(x => x.EndVerse)
                    .When(x => x.StartVerse.HasValue && x.EndVerse.HasValue && x.StartChapter!.Value == x.EndChapter!.Value)
                    .WithMessage(x => "'{PropertyName}' must be less than or equal to '{ComparisonProperty}' if verses from only one chapter are requested.");
            });
    }
}