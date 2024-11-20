using FastEndpoints;
using FluentValidation;

namespace Aquifer.API.Endpoints.TranslationPairs.Patch;

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x.Key).NotEmpty().When(x => x.Value is null);
        RuleFor(x => x.Value).NotEmpty().When(x => x.Key is null);
    }
}