using FastEndpoints;
using FluentValidation;

namespace Aquifer.API.Endpoints.Comments.Threads.List;

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x.TypeId).NotEmpty();
        RuleFor(x => x.ThreadType).IsInEnum();
    }
}