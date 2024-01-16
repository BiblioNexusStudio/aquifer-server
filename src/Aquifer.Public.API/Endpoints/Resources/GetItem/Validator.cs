﻿using FastEndpoints;
using FluentValidation;

namespace Aquifer.Public.API.Endpoints.Resources.GetItem;

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x.ContentId).GreaterThan(0);
    }
}