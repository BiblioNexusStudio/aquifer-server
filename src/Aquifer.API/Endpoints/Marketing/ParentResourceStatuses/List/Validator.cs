using FastEndpoints;
using FluentValidation;

namespace Aquifer.API.Endpoints.Marketing.ParentResourceStatuses.List;

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x.LanguageId).NotEmpty().InclusiveBetween(1, 32);
    }
}