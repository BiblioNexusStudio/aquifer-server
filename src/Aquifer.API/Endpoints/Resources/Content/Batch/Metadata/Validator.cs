using FastEndpoints;
using FluentValidation;

namespace Aquifer.API.Endpoints.Resources.Content.Batch.Metadata;

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x.Ids)
            .NotEmpty()
            .Must(ids => ids.Length <= 100);
    }
}