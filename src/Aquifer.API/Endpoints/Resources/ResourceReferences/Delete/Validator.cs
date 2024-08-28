using FastEndpoints;
using FluentValidation;

namespace Aquifer.API.Endpoints.Resources.ResourceReferences.Delete;

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x.ResourceContentId).GreaterThan(0);
        RuleFor(x => x.ReferenceResourceId).GreaterThan(0);
    }
}