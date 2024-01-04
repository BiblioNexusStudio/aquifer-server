using Aquifer.API.Common;
using Aquifer.API.Services;
using Aquifer.API.Utilities;
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

        return endpoints;
    }

    public IServiceCollection RegisterModule(IServiceCollection builder)
    {
        builder.AddHttpClient<IAuthProviderHttpService, AuthProviderHttpService>();
        return builder;
    }

    private async Task<Ok<List<UserResponse>>> GetAllUsers(AquiferDbContext dbContext,
        CancellationToken cancellationToken)
    {
        var users = await dbContext.Users.Select(user => new UserResponse
        {
            Id = user.Id, Name = $"{user.FirstName} {user.LastName}"
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
            Id = user.Id, Name = $"{user.FirstName} {user.LastName}", Permissions = permissions
        });
    }

    private async Task<Results<Created, BadRequest<string>>> CreateUser(AquiferDbContext dbContext,
        [FromBody] UserRequest user,
        IAuthProviderHttpService authProviderService,
        CancellationToken cancellationToken)
    {
        var tokenResponse = await authProviderService.GetAuth0Token(cancellationToken);
        var tokenResponseContent = await tokenResponse.Content.ReadAsStringAsync(cancellationToken);
        if (!tokenResponse.IsSuccessStatusCode)
        {
            return TypedResults.BadRequest($"Error getting Auth0 token: {tokenResponse.StatusCode} - {tokenResponseContent}");
        }

        var token = JsonUtilities.DefaultDeserialize<TokenResponse>(tokenResponseContent);

        var createUserResponse = await authProviderService.CreateUser(user, token.AccessToken, cancellationToken);
        string createUserResponseContent = await createUserResponse.Content.ReadAsStringAsync(cancellationToken);
        if (!createUserResponse.IsSuccessStatusCode)
        {
            return TypedResults.BadRequest("Auth0 threw error on user create");
        }

        var authUser = JsonUtilities.DefaultDeserialize<CreateUserResponse>(createUserResponseContent);

        await dbContext.Users.AddAsync(
            new UserEntity
            {
                Email = user.Email, FirstName = user.FirstName, LastName = user.LastName, ProviderId = authUser.UserId
            },
            cancellationToken);

        await dbContext.SaveChangesAsync(cancellationToken);
        return TypedResults.Created();
    }
}