using FastEndpoints;
using FluentValidation;

namespace Aquifer.API.Endpoints.Feedback.BibleWell.Create;

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Feedback).NotEmpty();
        RuleFor(x => x.FeedbackType).NotEmpty();
    }
}