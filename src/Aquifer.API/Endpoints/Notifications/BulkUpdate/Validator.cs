using FastEndpoints;
using FluentValidation;

namespace Aquifer.API.Endpoints.Notifications.BulkUpdate;

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x.Updates)
            .NotNull()
            .NotEmpty();

        RuleForEach(x => x.Updates)
            .ChildRules(
                u =>
                {
                    u.RuleFor(x => x.NotificationKind).IsInEnum();
                    u.RuleFor(x => x.NotificationKindId).GreaterThan(0);

                    // if we ever add future updatable properties to the request then this validation rule should be removed
                    u.RuleFor(x => x.IsRead).NotNull();
                });
    }
}