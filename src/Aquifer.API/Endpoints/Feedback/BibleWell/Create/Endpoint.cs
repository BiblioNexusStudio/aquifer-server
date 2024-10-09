using Aquifer.Data;
using Aquifer.Data.Entities;
using FastEndpoints;

namespace Aquifer.API.Endpoints.Feedback.BibleWell.Create;

public class Endpoint(AquiferDbContext dbContext) : Endpoint<Request>
{
    public override void Configure()
    {
        Post("/feedback/bible-well");
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var feedback = new FeedbackEntity
        {
            Name = req.Name,
            Email = req.Email,
            Phone = req.Phone,
            UserId = HttpContext.Request.Headers["bn-user-id"],
            Feedback = req.Feedback,
            FeedbackType = req.FeedbackType
        };

        await dbContext.Feedback.AddAsync(feedback, ct);
        await dbContext.SaveChangesAsync(ct);

        await SendNoContentAsync(ct);
    }
}