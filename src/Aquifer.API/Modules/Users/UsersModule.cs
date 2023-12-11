using Aquifer.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Aquifer.API.Modules.Users;

public class UsersModule : IModule
{
    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("users");
        group.MapGet("/", GetAllUsers).RequireAuthorization("read");
        group.MapGet("/self", GetCurrentUser).RequireAuthorization("read");

        return endpoints;
    }

    private async Task<Ok<List<BasicUserResponse>>> GetAllUsers(AquiferDbContext dbContext,
        CancellationToken cancellationToken)
    {
        var users = await dbContext.Users.Select(user => new BasicUserResponse
        {
            Id = user.Id,
            Name = $"{user.FirstName} {user.LastName}"
        }).ToListAsync(cancellationToken);

        return TypedResults.Ok(users);
    }

    private async Task<Results<Ok<BasicUserResponse>, NotFound>> GetCurrentUser(AquiferDbContext dbContext,
            ClaimsPrincipal claimsPrincipal,
            CancellationToken cancellationToken)
    {
        var providerId = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var user = await dbContext.Users.FirstOrDefaultAsync(u => u.ProviderId == providerId, cancellationToken);

        if (user == null)
        {
            return TypedResults.NotFound();
        }

        return TypedResults.Ok(new BasicUserResponse
        {
            Id = user.Id,
            Name = $"{user.FirstName} {user.LastName}"
        });
    }
}