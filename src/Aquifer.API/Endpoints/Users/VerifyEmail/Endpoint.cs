using Aquifer.Data;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Users.VerifyEmail;

public class Endpoint(AquiferDbContext dbContext, ILogger<Endpoint> logger) : Endpoint<Request>
{
    // This endpoint is used by Auth0 in the "Post Change Password" flow. Because we require a user
    // to set their password after account creation, verifying their email is essentially setting
    // a password. If there's an issue with it, for both the Development and Production tenants,
    // go to Auth0 portal Actions/Library/SetVerificationStatus to review it.

    public override void Configure()
    {
        Post("/users/verify-email");
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        if (req.ProviderId?.StartsWith("auth0|") is not true)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        var user = await dbContext.Users.SingleOrDefaultAsync(x => x.ProviderId == req.ProviderId, ct);
        if (user is null || user.EmailVerified)
        {
            logger.LogWarning("Tried to log update email verification but no user found or already set: {providerId}", req.ProviderId);
            await SendNotFoundAsync(ct);
            return;
        }

        user.EmailVerified = true;
        user.Updated = DateTime.UtcNow;
        await dbContext.SaveChangesAsync(ct);

        await SendNoContentAsync(ct);
    }
}