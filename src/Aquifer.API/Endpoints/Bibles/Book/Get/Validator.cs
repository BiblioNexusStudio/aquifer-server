using Aquifer.Common.Utilities;
using Aquifer.Data.Enums;
using FastEndpoints;
using FluentValidation;

namespace Aquifer.API.Endpoints.Bibles.Book.Get;

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x.BookCode)
            .Must(code => BibleBookCodeUtilities.IdFromCode(code) != BookId.None)
            .WithMessage("bookCode must be valid.");
    }
}