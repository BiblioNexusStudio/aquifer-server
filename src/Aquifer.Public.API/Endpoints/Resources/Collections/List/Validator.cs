using FastEndpoints;
using FluentValidation;

namespace Aquifer.Public.API.Endpoints.Resources.Collections.List;

public sealed class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x.ResourceType).IsInEnum();

        RuleFor(x => x.Offset).GreaterThanOrEqualTo(0);
        RuleFor(x => x.Limit).InclusiveBetween(1, Request.MaximumLimit);
    }
}