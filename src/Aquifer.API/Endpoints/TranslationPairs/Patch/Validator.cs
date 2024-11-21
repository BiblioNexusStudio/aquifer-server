using FastEndpoints;
using FluentValidation;

namespace Aquifer.API.Endpoints.TranslationPairs.Patch;

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x.Key).NotEmpty().When(x => x.Value is null);
        RuleFor(x => x.Value).NotEmpty().When(x => x.Key is null);
        RuleFor(x => x.Key).MinimumLength(3).When(x => x.Key is not null);
        RuleFor(x => x.Value).MinimumLength(3).When(x => x.Value is not null);
    }
}