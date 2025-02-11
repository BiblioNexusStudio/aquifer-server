using FastEndpoints;
using FluentValidation;

namespace Aquifer.API.Endpoints.Resources.Content.Create;

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x.EnglishLabel).NotEmpty();
        RuleFor(x => x.LanguageTitle).NotEmpty();
        RuleFor(x => x.LanguageId).GreaterThan(0);
        RuleFor(x => x.ParentResourceId).GreaterThan(0);
    }
}