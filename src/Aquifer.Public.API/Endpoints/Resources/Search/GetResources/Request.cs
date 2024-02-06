using System.ComponentModel;
using Aquifer.Data.Entities;

namespace Aquifer.Public.API.Endpoints.Resources.Search.GetResources;

public record Request
{
    /// <summary>
    ///     The keyword to search on. Currently only searches against content names (not inside content).
    ///     Non-English names are supported. Must be at least 3 characters in length. Optional if StartVerseId is provided.
    /// </summary>
    public string? Query { get; init; } = null!;

    /// <summary>
    ///     Optional book id based off USFM book numbers. Can get a list of available books and ids from the /bible-books endpoint.
    ///     Use this to search across an entire book, use startVerseId and endVerseId for more narrow results.
    /// </summary>
    public int? BookId { get; init; }

    /// <summary>
    ///     Optional verse id to search on. Resources often have relationships to verses or passages (however, there are
    ///     resources in the system that do not). Any resource that has a relationship to the provided range will be returned.
    ///     (e.g. a resource tied to Mark 1:3-5 would be found for a search across Mark 1:1-10). The Id should be prefixed by a 1,
    ///     then the book number using the standard USFM number, the chapter number, and the verse number. All values should be padded
    ///     with zeroes to make them length 3.
    ///     For example, If you were looking for Mark chapter 5 verse 13, the id would be 1042005013. You can get a list of book
    ///     numbers from the /bible-books endpoint.
    /// </summary>
    public int? StartVerseId { get; init; }

    /// <summary>
    ///     Same as the StartVerseId. If no value is provided it will search upon a single verse. If, for example, you wanted the whole book
    ///     of Mark, you could search from 1042000000 to 1043000000.
    /// </summary>
    public int? EndVerseId { get; init; }

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
    ///     The type of resource to search for. If none specified will default to None.
    ///     provided.
    /// </summary>
    public ResourceType ResourceType { get; init; }

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