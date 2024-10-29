using FastEndpoints;
using FluentValidation;

namespace Aquifer.API.Endpoints.Resources.Content.Status.Get;

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x.ContentId)
            .NotNull()
            .WithMessage("ContentId must not be null.")
            .NotEmpty()
            .WithMessage("ContentId must not be empty.")
            .GreaterThan(0)
            .WithMessage("ContentId must be greater than 0.");
    }
}