﻿using Aquifer.Data.Entities;

namespace Aquifer.Public.API.Endpoints.Resources.Search.GetResources;

public record Request
{
    /// <summary>
    ///     The keyword to search on. Currently only searches against content names (not inside content).
    ///     Non-English names are supported. Must be at least 3 characters in length.
    /// </summary>
    public string Query { get; init; } = null!;

    /// <summary>
    ///     Optional language id internal to Aquifer. If not specified, language code is required.
    /// </summary>
    public int LanguageId { get; init; }

    /// <summary>
    ///     Optional ISO 639-3 code that is 3 characters in length (e.g. "eng"). If not specified, language id is required.
    /// </summary>
    public string? LanguageCode { get; init; }

    /// <summary>
    ///     The type of resource to search for. If none specified will default to None. Either an int or string can be
    ///     provided.
    /// </summary>
    public ResourceType ResourceType { get; init; }

    /// <summary>
    ///     The number of results to return. Default is 10 and max is 100.
    /// </summary>
    public int Limit { get; init; } = 10;

    /// <summary>
    ///     Records to skip if paging through results. Default is 0.
    /// </summary>
    public int Offset { get; init; }
}