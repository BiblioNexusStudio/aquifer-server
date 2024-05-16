using FastEndpoints;
using FluentValidation;

namespace Aquifer.API.Endpoints.Resources.Content.AssignPublisherReview;

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x.AssignedUserId).NotNull().GreaterThan(0);
        RuleFor(x => x.ContentIds).NotNull().When(x => x.ContentId is null);
    }
}