using Aquifer.Common.Utilities;
using Aquifer.Data;
using FastEndpoints;

namespace Aquifer.API.Endpoints.Marketing.Unsubscribe;

public class Endpoint(AquiferDbContext dbContext, ILogger<Endpoint> logger) : Endpoint<Request>
{
    public override void Configure()
    {
        Get("/marketing/unsubscribe/{UnsubscribeId}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        try
        {
            var subscriberToDisable = dbContext.ContentSubscribers.SingleOrDefault(s => s.UnsubscribeId == req.UnsubscribeId);
            if (subscriberToDisable is not null)
            {
                subscriberToDisable.Enabled = false;
                await dbContext.SaveChangesAsync(ct);
            }
        }
        catch (Exception e)
        {
            logger.LogError(e, "Failed to handle subscriber request: {requestContent}", JsonUtilities.DefaultSerialize(req));
        }
        finally
        {
            await SendOkAsync("You have been successfully unsubscribed.", ct);
        }
    }
}