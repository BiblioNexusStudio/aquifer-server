﻿using System.ComponentModel;

namespace Aquifer.Public.API.Endpoints.Languages.AvailableResources.List;

public record Request
{
    /// <summary>
    /// Required book code based off USFM book identifier (e.g. GEN, EXO, etc.). Can get a list of available books and identifiers
    /// from the /bibles/books endpoint. Use this by itself to search across an entire book.
    /// </summary>
    public required string BookCode { get; init; }

    /// <summary>
    /// Optional start chapter to search from. If included, must also provide an end chapter. Required with verses.
    /// </summary>
    public int? StartChapter { get; init; }

    /// <summary>
    /// Optional end chapter to search from. Required with start chapter and verses.
    /// </summary>
    public int? EndChapter { get; init; }

    /// <summary>
    /// Optional start verse to search from. If included, must also provide an end verse and chapters.
    /// </summary>
    public int? StartVerse { get; init; }

    /// <summary>
    /// Optional end verse to search from. Required with start verse.
    /// </summary>
    public int? EndVerse { get; init; }

    /// <summary>
    /// Optional list of language codes to filter the results to.<br></br>
    /// Example: `languageCodes=eng&amp;languageCodes=fra`
    /// </summary>
    [DefaultValue("")]
    public string[] LanguageCodes { get; init; } = [];
}