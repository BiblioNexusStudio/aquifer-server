using FastEndpoints;
using FluentValidation;

namespace Aquifer.API.Endpoints.Resources.Content.List;

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x.LanguageId)
            .GreaterThan(0).When(x => x.LanguageId.HasValue);

        RuleFor(x => x.ParentResourceId)
            .GreaterThan(0).When(x => x.ParentResourceId.HasValue);

        RuleFor(x => x.BookCode)
            .NotEmpty().When(x => x.StartChapter.HasValue && x.EndChapter.HasValue)
            .WithMessage("BookCode must be specified if StartChapter and EndChapter are specified.");

        RuleFor(x => x.StartChapter)
            .GreaterThan(0).When(x => x.StartChapter.HasValue);

        RuleFor(x => x.EndChapter)
            .GreaterThan(0).When(x => x.EndChapter.HasValue);

        RuleFor(x => x)
            .Must(x => x.EndChapter!.Value >= x.StartChapter!.Value)
            .When(x => x.StartChapter.HasValue && x.EndChapter.HasValue)
            .WithMessage("EndChapter must be greater than or equal to StartChapter.");

        RuleFor(x => x)
            .Must(x => (x.StartChapter.HasValue && x.EndChapter.HasValue) || (!x.StartChapter.HasValue && !x.EndChapter.HasValue))
            .WithMessage("Both StartChapter and EndChapter must be specified together.");

        RuleFor(x => x.SearchQuery)
            .NotEmpty().When(x => x.SearchQuery != null)
            .WithMessage("SearchQuery must be non-empty if specified.");

        RuleFor(x => x)
            .Must(x => x.LanguageId.HasValue || x.ParentResourceId.HasValue ||
                       x.BookCode != null || x.IsPublished.HasValue || x.SearchQuery != null)
            .WithMessage("At least one parameter must be specified.");
    }
}