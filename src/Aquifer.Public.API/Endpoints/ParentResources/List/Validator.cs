using FastEndpoints;
using FluentValidation;

namespace Aquifer.Public.API.Endpoints.ParentResources.List;

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x)
            .Must(x => x.LanguageId is not null || x.LanguageCode is not null)
            .WithMessage("One of 'Language Id' or 'Language Code' is required.");

        RuleFor(x => x.LanguageId)
            .Empty()
            .When(x => x.LanguageCode is not null)
            .WithMessage("'{PropertyName}' must be empty when 'Language Code' is specified.");
        RuleFor(x => x.LanguageCode)
            .Empty()
            .When(x => x.LanguageId is not null)
            .WithMessage("'{PropertyName}' must be empty when 'Language Id' is specified.");

        RuleFor(x => x.LanguageId)
            .InclusiveBetween(1, 32);
        RuleFor(x => x.LanguageCode)
            .Length(3);
    }
}