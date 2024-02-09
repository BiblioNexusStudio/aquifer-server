using FastEndpoints;
using FluentValidation;

namespace Aquifer.API.Endpoints.Users.Create;

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x.FirstName).NotNull().MinimumLength(3);
        RuleFor(x => x.LastName).NotNull().MinimumLength(3);
        RuleFor(x => x.Email).NotNull().EmailAddress();
        RuleFor(x => x.Role).IsInEnum();
        RuleFor(x => x.CompanyId).GreaterThan(0);
    }
}