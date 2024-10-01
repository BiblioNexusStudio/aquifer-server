using Aquifer.Data;
using Aquifer.Data.Entities;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Feedback.Resources.Content.Create;

public class Endpoint(AquiferDbContext dbContext) : Endpoint<Request>
{
    public override void Configure()
    {
        Post("/feedback/resources/content");
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request request, CancellationToken ct)
    {
        var resourceContentVersion = await dbContext.ResourceContentVersions
            .AsTracking()
            .Where(rcv =>
                rcv.ResourceContentId == request.ContentId &&
                (rcv.Version == request.Version || (request.Version == null && rcv.IsPublished)))
            .FirstOrDefaultAsync(ct);

        if (resourceContentVersion == null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        var feedback = new ResourceContentVersionFeedbackEntity
        {
            ResourceContentVersion = resourceContentVersion,
            ContactValue = request.ContactValue,
            ContactType = request.ContactType,
            UserId = HttpContext.Request.Headers["bn-user-id"],
            Feedback = request.Feedback,
            UserRating = request.UserRating
        };

        await dbContext.ResourceContentVersionFeedback.AddAsync(feedback, ct);
        await dbContext.SaveChangesAsync(ct);

        await SendNoContentAsync(ct);
    }
}