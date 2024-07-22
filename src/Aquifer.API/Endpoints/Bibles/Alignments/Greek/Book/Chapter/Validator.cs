using Aquifer.Common.Utilities;
using Aquifer.Data.Enums;
using FastEndpoints;
using FluentValidation;

namespace Aquifer.API.Endpoints.Bibles.Alignments.Greek.Book.Chapter;

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x.BookCode)
            .Must(code => (int)BibleBookCodeUtilities.IdFromCode(code) is >= ((int)BookId.BookMAT) and
                          <= ((int)BookId.BookREV))
            .WithMessage("bookCode must be a valid New Testament bookCode");

        RuleFor(x => x.Chapter).InclusiveBetween(1, 28);
    }
}