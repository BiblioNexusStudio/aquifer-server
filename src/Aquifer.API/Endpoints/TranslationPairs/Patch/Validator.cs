using FastEndpoints;
using FluentValidation;

namespace Aquifer.API.Endpoints.TranslationPairs.Patch;

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x.Key).NotEmpty();
        RuleFor(x => x.Value).NotEmpty();
    }
}