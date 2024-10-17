using FastEndpoints;
using FluentValidation;

namespace Aquifer.API.Endpoints.Resources.Content.Versions.Get.StatusHistory;

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x.VersionId).NotEmpty();
    }
}