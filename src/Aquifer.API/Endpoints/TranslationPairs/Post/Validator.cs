using FastEndpoints;
using FluentValidation;

namespace Aquifer.API.Endpoints.TranslationPairs.Post;

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x.LanguageId).GreaterThan(0);
        RuleFor(x => x.Key).NotEmpty();
        RuleFor(x => x.Value).NotEmpty();
        RuleFor(x => x.Key).MinimumLength(3).When(x => x.Key is not null);
        RuleFor(x => x.Value).MinimumLength(3).When(x => x.Value is not null);
    }
}