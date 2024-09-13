using FastEndpoints;
using FluentValidation;

namespace Aquifer.API.Endpoints.Resources.Content.MachineTranslation.Create;

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x.ResourceContentVersionId).GreaterThan(0);
        // TODO: enable this after the deploy so we don't break translations for users with open pages
        // RuleFor(x => x.DisplayName).NotEmpty();
        RuleFor(x => x.Content).NotEmpty();
        RuleFor(x => x.SourceId).IsInEnum();
    }
}