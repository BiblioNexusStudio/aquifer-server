using System.ComponentModel;
using Aquifer.Data.Entities;

namespace Aquifer.Public.API.Endpoints.Resources.Search.GetResources;

public record Request
{
    /// <summary>
    ///     The keyword to search on. Currently only searches against content names (not inside content).
    ///     Non-English names are supported. Must be at least 3 characters in length. Optional if a bookCode or a resource type or collection code is provided.
    /// </summary>
    public string? Query { get; init; }

    /// <summary>
    ///     Book code based off USFM book identifier (e.g. GEN, EXO, etc.). Can get a list of available books and identifiers
    ///     from the /bibles/books endpoint. Use this by itself to search across an entire book. Required if no query parameter and no resource type or collection code are provided.
    /// </summary>
    public string? BookCode { get; init; }

    /// <summary>
    ///     Optional start chapter to search from. If included, must also provide an end chapter. Required with verses.
    /// </summary>
    [DefaultValue(0)]
    public int StartChapter { get; init; }

    /// <summary>
    ///     Optional end chapter to search from. Required with start chapter and verses.
    /// </summary>
    [DefaultValue(0)]
    public int EndChapter { get; init; }

    /// <summary>
    ///     Optional start verse to search from. If included, must also provide an end verse and chapters.
    /// </summary>
    [DefaultValue(0)]
    public int StartVerse { get; init; }

    /// <summary>
    ///     Optional end verse to search from. Required with start verse.
    /// </summary>
    [DefaultValue(0)]
    public int EndVerse { get; init; }

    /// <summary>
    ///     Optional language id internal to Aquifer. If not specified, language code is required.
    /// </summary>
    [DefaultValue(0)]
    public int LanguageId { get; init; }

    /// <summary>
    ///     Optional ISO 639-3 code that is 3 characters in length (e.g. "eng"). If not specified, language id is required.
    /// </summary>
    public string? LanguageCode { get; init; }

    /// <summary>
    ///     The type of resource to search for, such as "Dictionary". If none specified, will default to None.
    ///     If sending resourceType, do not send resourceCollectionCode.
    ///     A resource type or collection code is required if no book code and no query are provided.
    /// </summary>
    [DefaultValue(ResourceType.None)]
    public ResourceType ResourceType { get; init; }

    /// <summary>
    ///     Optional resource collection code to search for. Search by code retrieved from /resources/types endpoint such as
    ///     "TyndaleBibleDictionary". If sending resourceCollectionCode, do not send resourceType.
    ///     A resource type or collection code is required if no book code and no query are provided.
    /// </summary>
    public string? ResourceCollectionCode { get; init; }

    /// <summary>
    ///     The number of results to return. Max is 100.
    /// </summary>
    [DefaultValue(10)]
    public int Limit { get; init; } = 10;

    /// <summary>
    ///     Records to skip if paging through results.
    /// </summary>
    [DefaultValue(0)]
    public int Offset { get; init; }
}