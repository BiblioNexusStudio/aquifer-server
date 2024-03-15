using FastEndpoints;
using FluentValidation;

namespace Aquifer.API.Endpoints.Comments.Create;

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x.ThreadId).NotEmpty();
        RuleFor(x => x.Comment).NotEmpty();
    }
}