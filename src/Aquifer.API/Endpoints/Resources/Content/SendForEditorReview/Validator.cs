using FastEndpoints;
using FluentValidation;

namespace Aquifer.API.Endpoints.Resources.Content.SendForEditorReview;

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x.ContentIds).NotNull().When(x => x.ContentId is null);
        RuleFor(x => x.AssignedReviewerUserId).Null().When(x => x.SkipEditorStep);
    }
}