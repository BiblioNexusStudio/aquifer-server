using FastEndpoints;
using FluentValidation;

namespace Aquifer.API.Endpoints.Feedback.Resources.Content.Create;

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x.Feedback)
            .MaximumLength(250);

        RuleFor(x => x.Version)
            .Must(x => x is null or >= 1)
            .WithMessage("Version must be null or greater than or equal to 1");
    }
}