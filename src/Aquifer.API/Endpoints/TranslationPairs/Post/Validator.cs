using FastEndpoints;
using FluentValidation;

namespace Aquifer.API.Endpoints.TranslationPairs.Post;

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x.LanguageId).GreaterThan(0);
        RuleFor(x => x.Key).MinimumLength(3).NotEmpty();
        RuleFor(x => x.Value).MinimumLength(2).NotEmpty();
    }
}