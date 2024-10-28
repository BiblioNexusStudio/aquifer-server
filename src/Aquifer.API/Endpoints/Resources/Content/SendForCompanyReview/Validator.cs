using FastEndpoints;
using FluentValidation;

namespace Aquifer.API.Endpoints.Resources.Content.SendForCompanyReview;

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x).Must(x => x.ContentId is > 0 || x.ContentIds?.Count > 0);
    }
}