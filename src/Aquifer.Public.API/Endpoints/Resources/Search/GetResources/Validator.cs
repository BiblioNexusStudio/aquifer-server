using FastEndpoints;
using FluentValidation;

namespace Aquifer.Public.API.Endpoints.Resources.Search.GetResources;

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x.ResourceType).IsInEnum();
        RuleFor(x => x.Query).NotNull().MinimumLength(3).When(x => x.StartVerseId is null);
        RuleFor(x => x.StartVerseId).NotNull().When(x => x.Query is null);
        RuleFor(x => x.StartVerseId).InclusiveBetween(1001001001, 1099999999).When(x => x.StartVerseId is not null);
        RuleFor(x => x.EndVerseId).InclusiveBetween(1001001001, 1099999999).When(x => x.EndVerseId is not null);
        RuleFor(x => x.LanguageId).GreaterThan(0).When(x => x.LanguageCode is null);
        RuleFor(x => x.LanguageCode).Length(3).When(x => x.LanguageId == default);
        RuleFor(x => x.LanguageCode).Null().When(x => x.LanguageId > 0).WithMessage("Use either languageCode or languageId");
        RuleFor(x => x.Offset).GreaterThanOrEqualTo(0);
        RuleFor(x => x.Limit).InclusiveBetween(0, 100);
    }
}