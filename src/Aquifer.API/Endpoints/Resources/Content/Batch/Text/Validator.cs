using FastEndpoints;
using FluentValidation;

namespace Aquifer.API.Endpoints.Resources.Content.Batch.Text;

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x.Ids).NotEmpty().Must(ids => ids.Count <= 10).WithMessage("Number of Ids must be from 1 to 10");
    }
}