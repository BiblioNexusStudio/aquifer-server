using Aquifer.API.Clients.Http.Auth0;
using Aquifer.API.Common;
using Aquifer.Common.Utilities;
using Aquifer.Data;
using Aquifer.Data.Entities;
using FastEndpoints;

namespace Aquifer.API.Endpoints.Users.Create;

public class Endpoint(AquiferDbContext dbContext, IAuth0HttpClient authProviderService, ILogger<Endpoint> logger) : Endpoint<Request>
{
    public override void Configure()
    {
        Post("/users/create");
        Permissions(PermissionName.CreateUser);
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        await CreateUserAsync(req, ct);
        await SendAsync(null, 201, ct);
    }

    private async Task CreateUserAsync(Request req, CancellationToken ct)
    {
        var accessToken = await GetAccessTokenAsync(ct);
        var roleId = await GetRoleIdAsync(req, accessToken, ct);
        var newUserId = await CreateAuth0User(req, accessToken, ct);
        await AssignAuth0Role(accessToken, newUserId, roleId, ct);
        await SendPasswordReset(accessToken, req.Email, ct);

        await SaveUserToDatabase(req, newUserId, ct);
    }

    private async Task<string> GetAccessTokenAsync(CancellationToken ct)
    {
        var response = await authProviderService.GetAuth0Token(ct);
        var responseContent = await response.Content.ReadAsStringAsync(ct);
        if (!response.IsSuccessStatusCode)
        {
            logger.LogWarning("Error getting Auth0 token: {statusCode} - {response}", response.StatusCode, responseContent);
            ThrowError(responseContent, (int)response.StatusCode);
        }

        return JsonUtilities.DefaultDeserialize<Auth0TokenResponse>(responseContent).AccessToken;
    }

    private async Task<string> GetRoleIdAsync(Request req, string accessToken, CancellationToken ct)
    {
        var response = await authProviderService.GetUserRoles(accessToken, ct);
        var responseContent = await response.Content.ReadAsStringAsync(ct);
        if (!response.IsSuccessStatusCode)
        {
            logger.LogWarning("Error getting Auth0 roles: {statusCode} - {response}", response.StatusCode, responseContent);
            ThrowError(responseContent, (int)response.StatusCode);
        }

        var roles = JsonUtilities.DefaultDeserialize<List<Auth0AssignUserRolesResponse>>(responseContent);
        var role = roles.FirstOrDefault(r => r.Name == req.Role);
        if (role is null)
        {
            logger.LogWarning("Requested non-existent role: {requestedRole} - {response}", req.Role, responseContent);
            ThrowError("Requested role does not exist", 400);
        }

        return role.Id;
    }

    private async Task<string> CreateAuth0User(Request req, string accessToken, CancellationToken ct)
    {
        var response = await authProviderService.CreateUser($"{req.FirstName} {req.LastName}", req.Email, accessToken, ct);
        var responseContent = await response.Content.ReadAsStringAsync(ct);
        if (!response.IsSuccessStatusCode)
        {
            logger.LogWarning("Error creating Auth0 user: {statusCode} - {response}", response.StatusCode, responseContent);
            ThrowError(responseContent, (int)response.StatusCode);
        }

        return JsonUtilities.DefaultDeserialize<Auth0CreateUserResponse>(responseContent).UserId;
    }

    private async Task AssignAuth0Role(string accessToken, string userId, string roleId, CancellationToken ct)
    {
        var response = await authProviderService.AssignUserToRole(roleId, userId, accessToken, ct);

        var responseContent = await response.Content.ReadAsStringAsync(ct);
        if (!response.IsSuccessStatusCode)
        {
            logger.LogWarning("Unable to assign Auth0 user to role: {statusCode} - {response}", response.StatusCode, responseContent);

            ThrowError($"""
                        Auth0 threw an error on user role assignment.
                        Note that the Auth0 user has been created and recalling this
                        endpoint will result in different errors. Please ask for help.
                        {responseContent}
                        """,
                (int)response.StatusCode);
        }
    }

    private async Task SendPasswordReset(string accessToken, string email, CancellationToken ct)
    {
        // Auth0 doesn't support creating a user account without a password and having the user
        // create a password as part of the email verification. So we have to create a password
        // as part of creating their account, and then immediately send them the reset email
        // which will act as a creation / set password flow.

        var response = await authProviderService.ResetPassword(email, accessToken, ct);

        var responseContent = await response.Content.ReadAsStringAsync(ct);
        if (!response.IsSuccessStatusCode)
        {
            logger.LogWarning("Unable to reset Auth0 password on account creation: {statusCode} - {response}",
                response.StatusCode,
                responseContent);

            ThrowError($"""
                        Auth0 threw an error sending the password reset email.
                        Note that the Auth0 user has been created and recalling this
                        endpoint will result in different errors. Please ask for help.
                        {responseContent}
                        """,
                (int)response.StatusCode);
        }
    }

    private async Task SaveUserToDatabase(Request req, string providerUserId, CancellationToken ct)
    {
        await dbContext.Users.AddAsync(new UserEntity
            {
                Email = req.Email,
                FirstName = req.FirstName,
                LastName = req.LastName,
                ProviderId = providerUserId
            },
            ct);

        await dbContext.SaveChangesAsync(ct);
    }
}