using FastEndpoints;
using FluentValidation;

namespace Aquifer.API.Endpoints.Bibles.Texts.Get;

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x.BookCode).NotEmpty().When(x => x.BookNumber is null);
        RuleFor(x => x.BookNumber).NotEmpty().When(x => x.BookCode is null);
        RuleFor(x => x.StartChapter).LessThanOrEqualTo(x => x.EndChapter);
        RuleFor(x => x.StartVerse).LessThanOrEqualTo(x => x.EndVerse);
    }
}