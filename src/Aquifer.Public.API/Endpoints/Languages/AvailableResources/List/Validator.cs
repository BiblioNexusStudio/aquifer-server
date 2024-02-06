using FastEndpoints;
using FluentValidation;

namespace Aquifer.Public.API.Endpoints.Languages.AvailableResources.List;

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x)
            .Must(x => (x.BookId is not null && x.StartVerseId is null) || (x.StartVerseId is not null && x.BookId is null))
            .WithMessage("Either bookId or startVerseId is required. Cannot use both.");

        RuleFor(x => x.BookId).InclusiveBetween(1, 87).When(x => x.BookId is not null);
        RuleFor(x => x.StartVerseId).InclusiveBetween(1001001001, 1099999999).When(x => x.StartVerseId is not null);
        RuleFor(x => x.EndVerseId).InclusiveBetween(1001001001, 1099999999).When(x => x.EndVerseId is not null);
    }
}