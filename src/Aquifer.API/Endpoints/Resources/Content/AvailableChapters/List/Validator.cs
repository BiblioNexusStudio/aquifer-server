using FastEndpoints;
using FluentValidation;

namespace Aquifer.API.Endpoints.Resources.Content.AvailableChapters.List;

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x.LanguageId).NotEmpty();
        RuleFor(x => x.ParentResourceId).NotEmpty();
    }
}