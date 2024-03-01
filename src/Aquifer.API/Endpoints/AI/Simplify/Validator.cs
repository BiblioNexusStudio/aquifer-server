using FastEndpoints;
using FluentValidation;

namespace Aquifer.API.Endpoints.AI.Simplify;

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x.Prompt).NotEmpty();
        RuleFor(x => x.Content).NotEmpty();
    }
}