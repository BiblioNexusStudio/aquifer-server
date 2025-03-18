using FastEndpoints;
using FluentValidation;

namespace Aquifer.API.Endpoints.Resources.Content.NotApplicable.Update;

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x.ContentId).NotEmpty().GreaterThan(0);
        RuleFor(x => x.NotApplicableReason).NotEmpty();
    }
}