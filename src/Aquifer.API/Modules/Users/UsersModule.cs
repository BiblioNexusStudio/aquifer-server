using Aquifer.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Modules.Users;

public class UsersModule : IModule
{
    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("users");
        group.MapGet("/", GetAllUsers).RequireAuthorization("read");

        return endpoints;
    }

    private async Task<Ok<List<BasicUserResponse>>> GetAllUsers(AquiferDbContext dbContext,
        CancellationToken cancellationToken)
    {
        var users = await dbContext.Users.Select(user => new BasicUserResponse
        {
            Id = user.Id,
            Name = user.FirstName + " " + user.LastName
        }).ToListAsync(cancellationToken);

        return TypedResults.Ok(users);
    }
}