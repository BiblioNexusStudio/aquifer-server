﻿using FastEndpoints;
using FluentValidation;

namespace Aquifer.API.Endpoints.Resources.Content.MachineTranslation.Create;

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x.ResourceContentVersionId).GreaterThan(0);
        RuleFor(x => x.Content).NotEmpty();
        RuleFor(x => x.SourceId).IsInEnum();
    }
}