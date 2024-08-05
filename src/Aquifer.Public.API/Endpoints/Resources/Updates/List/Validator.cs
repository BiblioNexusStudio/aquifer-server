using FastEndpoints;
using FluentValidation;

namespace Aquifer.Public.API.Endpoints.Resources.Updates.List;

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x.Timestamp).NotEmpty().GreaterThanOrEqualTo(DateTime.UtcNow.AddDays(-90).Date);
        RuleFor(x => x.LanguageId).InclusiveBetween(1, 32);
    }
}