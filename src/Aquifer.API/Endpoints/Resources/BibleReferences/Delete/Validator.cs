using FastEndpoints;
using FluentValidation;

namespace Aquifer.API.Endpoints.Resources.BibleReferences.Delete;

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x.ResourceContentId).GreaterThan(0);
        RuleFor(x => x.StartVerseId).InclusiveBetween(1000000000, 1999999999);
        RuleFor(x => x.EndVerseId).InclusiveBetween(1000000000, 1999999999);
    }
}