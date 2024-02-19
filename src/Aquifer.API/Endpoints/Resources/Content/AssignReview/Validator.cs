using FastEndpoints;
using FluentValidation;

namespace Aquifer.API.Endpoints.Resources.Content.AssignReview;

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x.AssignedUserId).NotNull().GreaterThan(0);
    }
}