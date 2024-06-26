using Aquifer.Data;
using Aquifer.Data.Entities;
using FastEndpoints;

namespace Aquifer.API.Endpoints.FeedBack.BibleWell;

public class Endpoint(AquiferDbContext dbContext) : Endpoint<Request, Response>
{
    public override void Configure()
    {
        Post("/feedback/biblewell");
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var feedback = new FeedbackEntity
        {
            Name = req.Name,
            Email = req.Email,
            Phone = req.Phone,
            Feedback = req.Feedback
        };

        await dbContext.Feedback.AddAsync(feedback, ct);
        await dbContext.SaveChangesAsync(ct);

        await SendOkAsync(ct);
    }
}