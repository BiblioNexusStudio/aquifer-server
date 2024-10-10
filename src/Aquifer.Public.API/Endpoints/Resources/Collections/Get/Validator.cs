using FastEndpoints;
using FluentValidation;

namespace Aquifer.Public.API.Endpoints.Resources.Collections.Get;

public sealed class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x.Code)
            .NotEmpty();

        RuleFor(x => x.LanguageIds)
            .Null()
            .When(x => x.LanguageCodes is not null)
            .WithMessage("'{PropertyName}' must be empty when 'Language Codes' is specified.");
        RuleFor(x => x.LanguageCodes)
            .Null()
            .When(x => x.LanguageIds is not null)
            .WithMessage("'{PropertyName}' must be empty when 'Language Ids' is specified.");

        RuleFor(x => x.LanguageIds)
            .ForEach(id => id
                .NotEmpty()
                .InclusiveBetween(1, 32));
        RuleFor(x => x.LanguageCodes)
            .ForEach(c => c
                .NotEmpty()
                .Length(3));
    }
}