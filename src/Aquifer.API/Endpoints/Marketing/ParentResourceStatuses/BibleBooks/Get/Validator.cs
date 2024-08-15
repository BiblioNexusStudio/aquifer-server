using FastEndpoints;
using FluentValidation;

namespace Aquifer.API.Endpoints.Marketing.ParentResourceStatuses.BibleBooks.Get;

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x.LanguageId).NotEmpty().InclusiveBetween(1, 32);
        RuleFor(x => x.ParentResourceId).NotEmpty();
    }
}