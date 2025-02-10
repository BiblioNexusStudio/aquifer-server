using FastEndpoints;
using FluentValidation;

namespace Aquifer.API.Endpoints.Notifications.Update;

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x.NotificationKind).IsInEnum();
        RuleFor(x => x.NotificationEntityId).GreaterThan(0);
    }
}