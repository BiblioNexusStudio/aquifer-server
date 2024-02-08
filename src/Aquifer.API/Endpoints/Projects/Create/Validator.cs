using FastEndpoints;
using FluentValidation;

namespace Aquifer.API.Endpoints.Projects.Create;

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x.Title).NotEmpty();

        RuleFor(x => x.LanguageId).GreaterThan(0);

        RuleFor(x => x.ProjectManagerUserId).GreaterThan(0);

        RuleFor(x => x.CompanyId).GreaterThan(0);

        RuleFor(x => x.ProjectPlatformId).GreaterThan(0);

        RuleFor(x => x.CompanyLeadUserId).GreaterThan(0).When(x => x.CompanyLeadUserId.HasValue);

        RuleForEach(x => x.ResourceIds).GreaterThan(0);
    }
}