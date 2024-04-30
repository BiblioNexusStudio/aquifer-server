using FastEndpoints;
using FluentValidation;

namespace Aquifer.API.Endpoints.Resources.Content.MachineTranslation.Update;

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x.ResourceContentVersionId).GreaterThan(0);
        RuleFor(x => x.UserRating).InclusiveBetween((byte)1, (byte)5);
        RuleFor(x => x).Must(x => x is not { UserRating: null, ImproveClarity: null, ImproveConsistency: null, ImproveTone: null })
            .WithName("Properties")
            .WithMessage("Must update at least one property.");
    }
}