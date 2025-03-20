using FastEndpoints;
using FluentValidation;

namespace Aquifer.API.Endpoints.Admin.Caches.Clear;

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(r => r)
            .Must(r => r.ShouldClearMemoryCache || r.ShouldClearOutputCache)
            .WithMessage("At least one argument is required.");
    }
}