using Aquifer.API.Common;
using Aquifer.API.Services;
using Aquifer.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Modules.Users;

public class UsersModule : IModule
{
    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        var adminGroup = endpoints.MapGroup("admin/users").WithTags("Users (Admin)");
        adminGroup.MapGet("/", GetAllUsers).RequireAuthorization(PermissionName.ReadUsers);
        adminGroup.MapGet("/self", GetCurrentUser).RequireAuthorization();

        return endpoints;
    }

    private async Task<Ok<List<UserResponse>>> GetAllUsers(AquiferDbContext dbContext,
        CancellationToken cancellationToken)
    {
        var users = await dbContext.Users.Select(user => new UserResponse
        {
            Id = user.Id,
            Name = $"{user.FirstName} {user.LastName}"
        }).ToListAsync(cancellationToken);

        return TypedResults.Ok(users);
    }

    private async Task<Results<Ok<CurrentUserResponse>, NotFound>> GetCurrentUser(AquiferDbContext dbContext,
        IUserService userService,
        CancellationToken cancellationToken)
    {
        var user = await userService.GetUserFromJwtAsync(cancellationToken);
        var permissions = userService.GetAllPermissions();

        return TypedResults.Ok(new CurrentUserResponse
        {
            Id = user.Id,
            Name = $"{user.FirstName} {user.LastName}",
            Permissions = permissions
        });
    }
}