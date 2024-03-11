using FastEndpoints;
using FluentValidation;

namespace Aquifer.API.Endpoints.Resources.Content.Update;

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(r => r.DisplayName).NotEmpty();
    }
}