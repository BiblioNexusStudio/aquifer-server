using FastEndpoints;
using FluentValidation;

namespace Aquifer.API.Endpoints.Resources.Content.NextUp.Community;

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}