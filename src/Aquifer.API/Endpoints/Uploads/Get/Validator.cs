using FastEndpoints;
using FluentValidation;

namespace Aquifer.API.Endpoints.Uploads.Get;

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x.UploadId).GreaterThan(0);
    }
}