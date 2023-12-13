using Aquifer.API.Common;
using Aquifer.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Aquifer.API.Modules.Users;

public class UsersModule : IModule
{
    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        var adminGroup = endpoints.MapGroup("admin/users").WithTags("Users (Admin)");
        adminGroup.MapGet("/", GetAllUsers).RequireAuthorization(PermissionName.Read);
        adminGroup.MapGet("/self", GetCurrentUser).RequireAuthorization(PermissionName.Read);

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

    private async Task<Results<Ok<UserResponse>, NotFound>> GetCurrentUser(AquiferDbContext dbContext,
        ClaimsPrincipal claimsPrincipal,
        CancellationToken cancellationToken)
    {
        string? providerId = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var user = await dbContext.Users.FirstOrDefaultAsync(u => u.ProviderId == providerId, cancellationToken);

        if (user == null)
        {
            return TypedResults.NotFound();
        }

        return TypedResults.Ok(new UserResponse
        {
            Id = user.Id,
            Name = $"{user.FirstName} {user.LastName}"
        });
    }
}