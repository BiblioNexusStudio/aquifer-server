using FastEndpoints;
using FluentValidation;

namespace Aquifer.API.Endpoints.Resources.Search;

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x.LanguageId).NotEmpty();
        RuleFor(x => x.Query).NotEmpty().MinimumLength(3);
        RuleFor(x => x.ResourceTypes).NotEmpty();
        RuleForEach(x => x.ResourceTypes).IsInEnum();
    }
}