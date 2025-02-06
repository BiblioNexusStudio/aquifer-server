using Aquifer.API.Common;
using Aquifer.API.Services;
using Aquifer.Data.Entities;
using FastEndpoints;
using FluentValidation;

namespace Aquifer.API.Endpoints.Users.Role.Set;

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x.Role).IsInEnum();
        RuleFor(x => x.Role).Must(role =>
        {
            var userService = Resolve<IUserService>();
            return userService.HasPermission(PermissionName.CreateUser) || role == UserRole.Editor || role == UserRole.Reviewer;
        }).WithMessage("Managers can only assign editors or reviewers within their own company.");

        RuleFor(x => x.Role).Must(role => role != UserRole.Admin).WithMessage("Admins cannot be assigned from this endpoint.");
    }
}