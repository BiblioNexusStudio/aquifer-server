using FastEndpoints;
using FluentValidation;

namespace Aquifer.API.Endpoints.Resources.Content.Create;

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x.EnglishLabel).NotEmpty();
        RuleFor(x => x.LanguageTitle).NotNull().NotEmpty().When(x => x.LanguageId > 1);
    }
}