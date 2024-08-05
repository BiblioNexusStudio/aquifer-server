﻿using FastEndpoints;
using FluentValidation;

namespace Aquifer.API.Endpoints.Resources.ParentResources.Statuses.List;

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x.LanguageId).NotEmpty().InclusiveBetween(1, 32);
    }
}