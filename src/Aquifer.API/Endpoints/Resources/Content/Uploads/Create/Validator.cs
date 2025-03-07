using FastEndpoints;
using FluentValidation;

namespace Aquifer.API.Endpoints.Resources.Content.Uploads.Create;

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x.ResourceContentId).GreaterThan(0);
        RuleFor(x => x.StepNumber).GreaterThan(0).When(r => r.StepNumber.HasValue);
        RuleFor(x => x.File).NotNull();
    }
}