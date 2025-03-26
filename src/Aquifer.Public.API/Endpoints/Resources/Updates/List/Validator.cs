using FastEndpoints;
using FluentValidation;

#pragma warning disable CS0618 // Type or member is obsolete

namespace Aquifer.Public.API.Endpoints.Resources.Updates.List;

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x.StartTimestamp).NotNull().When(x => x.Timestamp is null);

        RuleFor(x => x.EndTimestamp)
            .GreaterThan(x => x.StartTimestamp)
            .When(x => x.StartTimestamp is not null)
            .WithMessage("EndTimestamp must be greater than StartTimestamp");

        RuleFor(x => x.EndTimestamp)
            .GreaterThan(x => x.Timestamp)
            .When(x => x.Timestamp is not null)
            .WithMessage("EndTimestamp must be greater than StartTimestamp. Timestamp is deprecated. Use StartTimestamp instead.");

        RuleFor(x => x.LanguageId).InclusiveBetween(1, 32);
        RuleFor(x => x.Offset).GreaterThanOrEqualTo(0);
        RuleFor(x => x.Limit).InclusiveBetween(1, 1000);
        RuleFor(x => x)
            .Must(
                x => (x.LanguageCode is not null && x.LanguageId == null) ||
                    x is { LanguageId: not null, LanguageCode: null } ||
                    (x.LanguageId == null && x.LanguageCode is null))
            .WithMessage("Cannot use both LanguageId and LanguageCode");
        RuleFor(x => x.LanguageCode).Length(3).When(x => x.LanguageCode is not null);
    }
}