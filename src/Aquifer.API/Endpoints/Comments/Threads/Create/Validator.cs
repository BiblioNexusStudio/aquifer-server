using FastEndpoints;
using FluentValidation;

namespace Aquifer.API.Endpoints.Comments.Threads.Create;

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x.Comment).NotEmpty();
        RuleFor(x => x.ThreadType).IsInEnum();
        RuleFor(x => x.TypeId).NotEmpty();
    }
}