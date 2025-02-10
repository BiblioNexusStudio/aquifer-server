using FastEndpoints;
using FluentValidation;

namespace Aquifer.API.Endpoints.Notifications.List;

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x.Offset).GreaterThanOrEqualTo(0);
        RuleFor(x => x.Limit).GreaterThan(0);
    }
}