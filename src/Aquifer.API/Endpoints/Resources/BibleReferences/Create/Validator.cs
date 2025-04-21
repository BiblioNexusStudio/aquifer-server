using FastEndpoints;
using FluentValidation;

namespace Aquifer.API.Endpoints.Resources.BibleReferences.Create;

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x.ResourceContentId).GreaterThan(0);
        RuleFor(x => x.StartVerseId).InclusiveBetween(1000000000, 1999999999);
        RuleFor(x => x.EndVerseId).InclusiveBetween(1000000000, 1999999999);
        RuleFor(x => x.EndVerseId)
            .GreaterThanOrEqualTo(x => x.StartVerseId)
            .WithMessage("End verse ID must be greater than or equal to start verse ID.");
    }
}