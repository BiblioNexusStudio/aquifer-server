﻿using Aquifer.Common.Utilities;
using Aquifer.Data.Enums;
using FastEndpoints;
using FluentValidation;

namespace Aquifer.Public.API.Endpoints.Resources.Search.GetResources;

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x).Must(x => x.Query is not null || x.BookCode is not null)
            .WithMessage("Either search query or bookCode is required.");

        RuleFor(x => x)
            .Must(x => (x.LanguageCode is not null && x.LanguageId == default) || (x.LanguageId != default && x.LanguageCode is null))
            .WithMessage("Either languageId or languageCode is required. Cannot use both.");

        RuleFor(x => x.BookCode).NotNull().When(x => x.StartChapter > 0);
        RuleFor(x => x.BookCode).Must(x => BibleBookCodeUtilities.IdFromCode(x!) != BookId.None)
            .WithMessage("Invalid book code {PropertyValue}. Get a valid list from /bible-books endpoint.")
            .When(x => x.BookCode is not null);

        RuleFor(x => x.StartChapter).InclusiveBetween(0, 150);
        RuleFor(x => x.EndChapter).InclusiveBetween(0, 150);
        RuleFor(x => x.StartVerse).InclusiveBetween(0, 200);
        RuleFor(x => x.EndVerse).InclusiveBetween(0, 200);

        RuleFor(x => x.StartChapter).GreaterThan(0).When(x => x.EndChapter > 0 || x.StartVerse > 0);
        RuleFor(x => x.EndChapter).GreaterThan(0).When(x => x.StartChapter > 0);
        RuleFor(x => x.StartVerse).GreaterThan(0).When(x => x.EndVerse > 0);
        RuleFor(x => x.EndVerse).GreaterThan(0).When(x => x.StartVerse > 0);

        RuleFor(x => x).Must(x => x.StartChapter <= x.EndChapter)
            .WithMessage("startChapter cannot be greater than endChapter");

        RuleFor(x => x.Query).MinimumLength(3).When(x => x.Query is not null);
        RuleFor(x => x.ResourceType).IsInEnum();
        RuleFor(x => x.LanguageId).GreaterThan(0).When(x => x.LanguageCode is null);
        RuleFor(x => x.LanguageCode).Length(3).When(x => x.LanguageId == default);
        RuleFor(x => x.Offset).GreaterThanOrEqualTo(0);
        RuleFor(x => x.Limit).InclusiveBetween(0, 100);
    }
}