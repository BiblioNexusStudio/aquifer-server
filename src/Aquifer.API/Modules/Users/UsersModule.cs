using Aquifer.API.Common;
using Aquifer.API.Services;
using Aquifer.Common.Utilities;
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
        adminGroup.MapPost("/create", CreateUser).RequireAuthorization(PermissionName.CreateUser);

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
        var users = await dbContext.Users.Select(user => new UserResponse { Id = user.Id, Name = $"{user.FirstName} {user.LastName}" })
            .ToListAsync(cancellationToken);

        return TypedResults.Ok(users);
    }

    private async Task<Results<Ok<CurrentUserResponse>, NotFound>> GetCurrentUser(AquiferDbContext dbContext,
        IUserService userService,
        CancellationToken cancellationToken)
    {
        var user = await userService.GetUserFromJwtAsync(cancellationToken);
        var permissions = userService.GetAllJwtPermissions();
        var roles = userService.GetAllJwtRoles();

        return TypedResults.Ok(new CurrentUserResponse
        {
            Id = user.Id,
            Name = $"{user.FirstName} {user.LastName}",
            Permissions = permissions,
            Roles = roles
        });
    }

    private async Task<Results<Created, BadRequest<string>>> CreateUser(AquiferDbContext dbContext,
        [FromBody] CreateUserRequest user,
        IAuthProviderHttpService authProviderService,
        CancellationToken cancellationToken)
    {
        var tokenResponse = await authProviderService.GetAuth0Token(cancellationToken);
        string tokenResponseContent = await tokenResponse.Content.ReadAsStringAsync(cancellationToken);
        if (!tokenResponse.IsSuccessStatusCode)
        {
            return TypedResults.BadRequest(
                $"Error getting Auth0 token: {tokenResponse.StatusCode} - {tokenResponseContent}");
        }

        var token = JsonUtilities.DefaultDeserialize<TokenResponse>(tokenResponseContent);

        var getRolesResponse = await authProviderService.GetUserRoles(token.AccessToken, cancellationToken);
        string getRolesResponseContent = await getRolesResponse.Content.ReadAsStringAsync(cancellationToken);
        if (!getRolesResponse.IsSuccessStatusCode)
        {
            return TypedResults.BadRequest(
                $"Error getting Auth0 roles: {getRolesResponse.StatusCode} - {getRolesResponseContent}");
        }

        var roles = JsonUtilities.DefaultDeserialize<List<GetRolesResponse>>(getRolesResponseContent);
        var role = roles.FirstOrDefault(r => r.Name == user.Role);
        if (role is null)
        {
            return TypedResults.BadRequest($"Role {user.Role} does not exist");
        }

        var createUserResponse = await authProviderService.CreateUser(user, token.AccessToken, cancellationToken);
        string createUserResponseContent = await createUserResponse.Content.ReadAsStringAsync(cancellationToken);
        if (!createUserResponse.IsSuccessStatusCode)
        {
            return TypedResults.BadRequest(
                $"Auth0 threw error on user create: : {createUserResponse.StatusCode} - {createUserResponseContent}");
        }

        var authUser = JsonUtilities.DefaultDeserialize<CreateUserResponse>(createUserResponseContent);

        var assignUserToRoleResponse =
            await authProviderService.AssignUserToRole(role.Id,
                authUser.UserId,
                token.AccessToken,
                cancellationToken);

        string assignUserToRoleResponseContent =
            await assignUserToRoleResponse.Content.ReadAsStringAsync(cancellationToken);
        if (!assignUserToRoleResponse.IsSuccessStatusCode)
        {
            return TypedResults.BadRequest($"""
                                            Auth0 threw error on user role assignment.
                                            Please note that the Auth0 user has been created and recalling this
                                            endpoint will result in different errors.
                                            {assignUserToRoleResponse.StatusCode} - {assignUserToRoleResponseContent}
                                            """);
        }

        await dbContext.Users.AddAsync(
            new UserEntity { Email = user.Email, FirstName = user.FirstName, LastName = user.LastName, ProviderId = authUser.UserId },
            cancellationToken);

        await dbContext.SaveChangesAsync(cancellationToken);
        return TypedResults.Created();
    }
}