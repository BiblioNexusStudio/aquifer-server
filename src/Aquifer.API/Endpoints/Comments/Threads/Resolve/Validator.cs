using FastEndpoints;
using FluentValidation;

namespace Aquifer.API.Endpoints.Comments.Threads.Resolve;

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x.ThreadId).NotEmpty();
    }
}