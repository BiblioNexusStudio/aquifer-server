using FastEndpoints;
using FluentValidation;

namespace Aquifer.API.Endpoints.FeedBack.BibleWell.Create;
public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Feedback).NotEmpty();
    }
}
