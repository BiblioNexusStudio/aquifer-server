using FastEndpoints;
using FluentValidation;

namespace Aquifer.API.Endpoints.Projects.Create;

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x.Title).NotEmpty();
        RuleFor(x => x.SourceLanguageId).GreaterThan(0);
        RuleFor(x => x.TargetLanguageId).GreaterThan(0).When(x => !x.IsAlreadyTranslated);
        RuleFor(x => x.TargetLanguageId).Null().When(x => x.IsAlreadyTranslated);
        RuleFor(x => x.ProjectManagerUserId).GreaterThan(0);
        RuleFor(x => x.CompanyId).GreaterThan(0);
        RuleFor(x => x.CompanyLeadUserId).GreaterThan(0);
        RuleForEach(x => x.ResourceIds).GreaterThan(0);
    }
}