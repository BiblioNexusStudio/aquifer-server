using FastEndpoints;
using FluentValidation;

namespace Aquifer.API.Endpoints.Projects.Update;

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x.QuotedCost)
            .PrecisionScale(int.MaxValue, 2, true);
    }
}