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
        var emailValue = req.Email is null && (req.ContactType == FeedbackContactType.Email) ? req.ContactValue : req.Email;
        var phoneValue = req.Phone is null && (req.ContactType == FeedbackContactType.Phone) ? req.ContactValue : req.Phone;
        var feedback = new FeedbackEntity
        {
            Name = req.Name,
            Email = emailValue,
            Phone = phoneValue,
            UserId = HttpContext.Request.Headers["bn-user-id"],
            Feedback = req.Feedback,
            FeedbackType = req.FeedbackType,
            ContactType = req.ContactType,
            ContactValue = req.ContactValue
        };

        await dbContext.Feedback.AddAsync(feedback, ct);
        await dbContext.SaveChangesAsync(ct);

        await SendNoContentAsync(ct);
    }
}