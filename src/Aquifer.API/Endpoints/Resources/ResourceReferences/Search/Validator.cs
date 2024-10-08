using FastEndpoints;
using FluentValidation;

namespace Aquifer.API.Endpoints.Resources.ResourceReferences.Search;

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x.ParentResourceId).GreaterThan(0);
        RuleFor(x => x.Query).NotEmpty();
    }
}