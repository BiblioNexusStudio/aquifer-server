using FastEndpoints;
using FluentValidation;

namespace Aquifer.API.Endpoints.Reports.Dynamic.Get;

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x.EndDate)
            .GreaterThan(x => x.StartDate).WithMessage("End date must be after start date");
        RuleFor(x => x.LanguageId).GreaterThanOrEqualTo(0);
        RuleFor(x => x.ParentResourceId).GreaterThanOrEqualTo(0);
    }
}