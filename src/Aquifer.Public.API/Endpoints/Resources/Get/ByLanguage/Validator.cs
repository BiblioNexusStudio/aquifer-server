using FastEndpoints;
using FluentValidation;

namespace Aquifer.Public.API.Endpoints.Resources.Get.ByLanguage;

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x.ContentId).GreaterThan(0);
        RuleFor(x => x.ContentTextType).IsInEnum();
        RuleFor(x => x.LanguageCode).NotEmpty().Length(3);
    }
}