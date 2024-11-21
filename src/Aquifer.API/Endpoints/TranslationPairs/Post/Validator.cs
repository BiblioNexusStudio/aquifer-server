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
    }
}