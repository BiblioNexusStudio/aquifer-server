using FastEndpoints;
using FluentValidation;

namespace Aquifer.API.Endpoints.Marketing.Unsubscribe;

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x.UnsubscribeId).NotEmpty().Length(64);
    }
}