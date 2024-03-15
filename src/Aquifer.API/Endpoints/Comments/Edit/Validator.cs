using FastEndpoints;
using FluentValidation;

namespace Aquifer.API.Endpoints.Comments.Edit;

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x.Comment).NotEmpty();
        RuleFor(x => x.CommentId).NotEmpty();
    }
}