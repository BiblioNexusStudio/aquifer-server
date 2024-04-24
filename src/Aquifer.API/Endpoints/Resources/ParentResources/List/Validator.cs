using FastEndpoints;
using FluentValidation;

namespace Aquifer.API.Endpoints.Resources.ParentResources.List;

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x.LanguageId).NotEmpty();
    }
}