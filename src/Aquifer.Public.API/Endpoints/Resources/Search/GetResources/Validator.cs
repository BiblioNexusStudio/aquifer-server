using FastEndpoints;
using FluentValidation;

namespace Aquifer.Public.API.Endpoints.Resources.Search.GetResources;

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x.ResourceType).IsInEnum();
        RuleFor(x => x.Query).MinimumLength(3);
    }
}