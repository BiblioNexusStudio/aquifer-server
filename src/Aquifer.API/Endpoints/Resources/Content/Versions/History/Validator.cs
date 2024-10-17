using FastEndpoints;
using FluentValidation;

namespace Aquifer.API.Endpoints.Resources.Content.Versions.History;

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x.VersionId).NotEmpty();
    }
}