using FastEndpoints;
using FluentValidation;

namespace Aquifer.API.Endpoints.Projects.Update;

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x.QuotedCost)
            .PrecisionScale(int.MaxValue, 2, true);

        RuleFor(x => x).Must(x =>
                x.QuotedCost is not null ||
                x.ProjectManagerUserId is not null ||
                x.ProjectedDeliveryDate is not null ||
                x.ProjectedPublishDate is not null ||
                x.CompanyLeadUserId is not null ||
                x.EffectiveWordCount is not null)
            .WithMessage("No values set");
    }
}