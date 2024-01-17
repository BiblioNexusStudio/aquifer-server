using Aquifer.Data.Entities;
using FastEndpoints;
using FluentValidation;

namespace Aquifer.Public.API.Endpoints.Resources.Search.GetResources;

public class Validator : Validator<Request>
{
    private static readonly string ResourceTypes =
        string.Join(", ", Enum.GetValues(typeof(ResourceType)).Cast<ResourceType>().ToList());

    public Validator()
    {
        RuleFor(x => x.ResourceType).IsInEnum();
        RuleFor(x => x.Query).NotNull().MinimumLength(3);
        RuleFor(x => x.LanguageId).GreaterThan(0).When(x => x.LanguageCode is null);
        RuleFor(x => x.LanguageCode).Length(3).When(x => x.LanguageId == default);
        RuleFor(x => x.LanguageCode).Null().When(x => x.LanguageId > 0)
            .WithMessage("Use either languageCode or languageId");
        RuleFor(x => x.Offset).GreaterThanOrEqualTo(0);
        RuleFor(x => x.Limit).InclusiveBetween(0, 100);
    }
}