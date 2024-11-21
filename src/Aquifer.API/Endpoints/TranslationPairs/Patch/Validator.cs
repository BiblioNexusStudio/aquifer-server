using FastEndpoints;
using FluentValidation;

namespace Aquifer.API.Endpoints.TranslationPairs.Patch;

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x.Key).MinimumLength(3).NotEmpty().When(x => x.Value is null);
        RuleFor(x => x.Value).MinimumLength(3).NotEmpty().When(x => x.Key is null);
    }
}