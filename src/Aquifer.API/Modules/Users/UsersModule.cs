using Aquifer.API.Common;
using Aquifer.API.Services;
using Aquifer.Data;
using Aquifer.Data.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Modules.Users;

public class UsersModule : IModule
{
    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        var adminGroup = endpoints.MapGroup("admin/users").WithTags("Users (Admin)");
        adminGroup.MapGet("/", GetAllUsers).RequireAuthorization(PermissionName.ReadUsers);
        adminGroup.MapGet("/self", GetCurrentUser).RequireAuthorization();
        adminGroup.MapPost("/create", CreateUser);
        adminGroup.MapGet("/test", Test);

        return endpoints;
    }

    private async Task<Ok<string>> Test(IAzureKeyVaultService keyVault)
    {
        string secret = await keyVault.GetSecretAsync(KeyVaultSecretName.Auth0ClientSecret);
        return TypedResults.Ok(secret);
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
        var permissions = userService.GetAllJwtPermissions();

        return TypedResults.Ok(new CurrentUserResponse
        {
            Id = user.Id,
            Name = $"{user.FirstName} {user.LastName}",
            Permissions = permissions
        });
    }

    private async Task<Ok> CreateUser(AquiferDbContext dbContext,
        [FromBody] UserRequest user,
        IAuthProviderHttpService authProviderService,
        CancellationToken cancellationToken)
    {
        string providerId = await authProviderService.CreateUser(user, cancellationToken);

        await dbContext.Users.AddAsync(new UserEntity
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                ProviderId = providerId
            },
            cancellationToken);

        await dbContext.SaveChangesAsync(cancellationToken);
        return TypedResults.Ok();
    }
}