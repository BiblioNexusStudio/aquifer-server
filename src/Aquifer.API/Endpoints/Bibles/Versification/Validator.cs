using FastEndpoints;
using FluentValidation;

namespace Aquifer.API.Endpoints.Bibles.Versification;

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x.BookId).NotEmpty();
        RuleFor(x => x.StartChapter).NotEmpty().When(x => x.EndChapter.HasValue);
        RuleFor(x => x.StartChapter).LessThanOrEqualTo(x => x.EndChapter); 
        RuleFor(x => x.StartVerse).LessThanOrEqualTo(x => x.EndVerse).When(x => x.StartChapter == x.EndChapter);
    }
}