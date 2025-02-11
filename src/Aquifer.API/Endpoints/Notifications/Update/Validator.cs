using FastEndpoints;
using FluentValidation;

namespace Aquifer.API.Endpoints.Notifications.Update;

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x.NotificationKind).IsInEnum();
        RuleFor(x => x.NotificationKindId).GreaterThan(0);

        // if we ever add future updatable properties to the request then this validation rule should be removed
        RuleFor(x => x.IsRead).NotNull();
    }
}