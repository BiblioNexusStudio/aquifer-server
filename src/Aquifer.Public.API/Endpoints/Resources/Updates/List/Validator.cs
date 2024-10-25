using FastEndpoints;
using FluentValidation;

namespace Aquifer.Public.API.Endpoints.Resources.Updates.List;

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x.Timestamp).NotEmpty().GreaterThanOrEqualTo(DateTime.UtcNow.AddDays(-90).Date);
        RuleFor(x => x.LanguageId).InclusiveBetween(1, 32);
        RuleFor(x => x.Offset).GreaterThanOrEqualTo(0);
        RuleFor(x => x.Limit).InclusiveBetween(0, 1000);
        RuleFor(x => x)
            .Must(x => (x.LanguageCode is not null && x.LanguageId == null)
                       || (x is { LanguageId: not null, LanguageCode: null })
                       || (x.LanguageId == null && x.LanguageCode is null))
            .WithMessage("Cannot use both languageId and languageCode");
    }
}