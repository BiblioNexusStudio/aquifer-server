using FastEndpoints;
using FluentValidation;

namespace Aquifer.API.Endpoints.Resources.Content.Update;

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x).Must(x =>
            x.DisplayName is not null || x.Content is not null || x.WordCount is not null);
    }
}