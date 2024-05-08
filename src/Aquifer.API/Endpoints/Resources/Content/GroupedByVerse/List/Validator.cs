using Aquifer.Common.Utilities;
using Aquifer.Data.Enums;
using FastEndpoints;
using FluentValidation;

namespace Aquifer.API.Endpoints.Resources.Content.GroupedByVerse.List;

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x.LanguageId).NotEmpty();
        RuleFor(x => x.BookCode)
            .Must(code => code != null && BibleBookCodeUtilities.IdFromCode(code) != BookId.None)
            .WithMessage("bookCode must be valid.");
        RuleFor(x => x.Chapter).InclusiveBetween(1, 150);
    }
}