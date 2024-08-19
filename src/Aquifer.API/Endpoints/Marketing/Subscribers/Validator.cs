using FastEndpoints;
using FluentValidation;

namespace Aquifer.API.Endpoints.Marketing.Subscribers;

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(256);
        RuleFor(x => x.Email).NotEmpty().MaximumLength(256).EmailAddress();
        RuleFor(x => x.Organization).MaximumLength(256);
    }
}