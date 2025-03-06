using Aquifer.API.Common;
using Aquifer.API.Services;
using Aquifer.Data.Entities;
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
        RuleFor(x => x.LanguageId).GreaterThan(0);
        RuleFor(x => x.Role).Must(role =>
        {
            var userService = Resolve<IUserService>();
            return userService.HasPermission(PermissionName.CreateUser) || role == UserRole.Editor || role == UserRole.Reviewer;
        }).WithMessage("Managers can only create editors or reviewers within their own company.");

        RuleFor(x => x.Role).Must(role => role != UserRole.Admin).WithMessage("Admins cannot be created from this endpoint.");
    }
}