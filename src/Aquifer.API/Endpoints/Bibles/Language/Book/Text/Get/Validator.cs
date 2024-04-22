using Aquifer.Data.Enums;
using FastEndpoints;
using FluentValidation;

namespace Aquifer.API.Endpoints.Bibles.Language.Book.Text.Get;

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x.BookId)
            .Must(id => id != BookId.None)
            .WithMessage("bookId must be valid.");
    }
}