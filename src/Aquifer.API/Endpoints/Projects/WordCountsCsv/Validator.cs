using FastEndpoints;
using FluentValidation;

namespace Aquifer.API.Endpoints.Projects.WordCountsCsv;

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x.ProjectId).NotEmpty();
    }
}