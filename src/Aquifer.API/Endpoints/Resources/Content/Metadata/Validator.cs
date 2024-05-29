using FastEndpoints;
using FluentValidation;

namespace Aquifer.API.Endpoints.Resources.Content.Metadata;

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x.ContentId).NotEmpty();
    }
}