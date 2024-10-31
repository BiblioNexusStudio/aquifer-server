using FastEndpoints;
using FluentValidation;

namespace Aquifer.API.Endpoints.AI.Translate;

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x.LanguageName).NotEmpty();
        RuleFor(x => x.Content).NotEmpty();
    }
}